using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Odyssey.Controls;
using Odyssey.Controls.Classes;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Services;
using WaveTech.Dafuscator.WpfApplication.Classes;
using WaveTech.Dafuscator.WpfApplication.UserControls;

namespace WaveTech.Dafuscator.WpfApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RibbonWindow
	{
		#region Dependency Properties
		public static readonly DependencyProperty SelectedTableProperty =
				DependencyProperty.Register("SelectedTable", typeof(Model.Table), typeof(MainWindow),
				new FrameworkPropertyMetadata(
						null,
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
						SelectedTablePropertyChanged));
		#endregion Dependency Properties

		#region Constructor
		public MainWindow()
		{
			try
			{
				InitializeComponent();
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}

			EventManager.RegisterClassHandler(typeof(TreeViewItem),
								Mouse.MouseDownEvent, new MouseButtonEventHandler(OnTreeViewItemMouseDown), false);

			try
			{
				IconBitmapDecoder ibd = new IconBitmapDecoder(
				new Uri(@"pack://application:,,/DafuscatorIcon.ico", UriKind.RelativeOrAbsolute),
				BitmapCreateOptions.None,
				BitmapCacheOption.Default);
				Icon = ibd.Frames[0];
			}
			catch (Exception ex) { }

			SkinManager.SkinId = SkinId.OfficeBlack;

			SetWindowTitle();
			Initialize();
		}
		#endregion Constructor

		#region Public Properties
		public Model.Table SelectedTable
		{
			get { return (Model.Table)GetValue(SelectedTableProperty); }
			set { SetValue(SelectedTableProperty, value); }
		}
		#endregion Public Properties

		#region Private Members
		private void Initialize()
		{



			RefreshData();

			// TODO: Remove me!
			//ColumnsGrid.Visibility = Visibility.Visible;
		}

		public void RefreshData()
		{
			foreach (string res in new string[] {
                "databaseData",
								"generatorData"
            })
			{
				ObjectDataProvider provider = Resources[res] as ObjectDataProvider;
				if (provider != null)
				{
					provider.InitialLoad();
					provider.Refresh();
				}
			}
		}

		private void SetWindowTitle()
		{
#if CE
			this.Title = "Dafuscator (Community Edition)";
#else
			this.Title = "Dafuscator (Professional Edition)";
#endif
		}
		#endregion Private Members

		#region Private Event Handlers
		private static void OnTreeViewItemMouseDown(object sender, MouseButtonEventArgs e)
		{
			var item = sender as TreeViewItem;
			if (e.RightButton == MouseButtonState.Pressed)
			{
				item.IsSelected = true;
				e.Handled = true;
			}
		}

		protected void OnSelectedTableChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			WaveTech.Dafuscator.Model.Table t = e.NewValue as WaveTech.Dafuscator.Model.Table;
			SelectedTable = t;
		}

		private static void SelectedTablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MainWindow)d).OnSelectedTableChanged(d, (WaveTech.Dafuscator.Model.Table)e.OldValue, (WaveTech.Dafuscator.Model.Table)e.NewValue);
		}

		protected virtual void OnSelectedTableChanged(object sender, Model.Table oldTable, Model.Table newTable)
		{
			if (newTable != null)
			{
				newTable.PropertyChanged += SelectedTablePropertyChanged;
			}
			if (oldTable != null)
			{
				oldTable.PropertyChanged -= SelectedTablePropertyChanged;
			}

			this.ColumnsGrid = new ColumnsGrid();
			this.ColumnsGrid.SelectedTable = newTable;
			this.ColumnsGrid.DataContext = newTable;
		}

		private void SelectedTablePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			// some bindings have IsAsync=True, so we need to do this invoked:
			Dispatcher.BeginInvoke((Func<int>)delegate()
			{
				Model.Table table = (Model.Table)sender;
				return 0;
			});
		}

		private void newWindow_CancelButtonClick(object sender, RoutedEventArgs e)
		{
			newWindow.ResetForm();
			newWindow.Visibility = Visibility.Collapsed;
		}

		private void newWindow_ConnectButtonClick(object sender, RoutedEventArgs e)
		{
			if (newWindow.IsFormValid())
			{
				BackgroundWorker worker = new BackgroundWorker();

				newWindow.loadingAnimation.Visibility = Visibility.Visible;

				IDatabaseInteractionService dbService = ObjectLocator.GetInstance<IDatabaseInteractionService>();

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
													{
														ConnectionString connectionString = args.Argument as ConnectionString;
														bool valid = dbService.TestConnection(connectionString);
														args.Result = valid;

														if (valid)
														{
															if (newWindow.IsUpdateOnly)
															{
																UIContext.Database.ConnectionString = connectionString;
															}
															else
															{

																Database db = new Database();
																db.ConnectionString = connectionString;

																UIContext.Database = db;

																RefreshData();
															}
														}
													};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
																			{
																				if (args.Result != null && !(bool)args.Result)
																				{
																					newWindow.loadingAnimation.Visibility = Visibility.Collapsed;

																					MessageBox.Show(
																						"There was a problem connecting to the database\r\nusing the information you supplied.\r\n\r\nPlease checking the information and retry.",
																						"Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
																				}
																				else if (args.Result != null)
																				{
																					newWindow.ResetForm();
																					newWindow.Visibility = Visibility.Collapsed;

																					newWindow.loadingAnimation.Visibility = Visibility.Collapsed;
																					ColumnsGrid.Visibility = Visibility.Visible;
																				}
																			};

				worker.RunWorkerAsync(newWindow.GetConnectionString());
			}
		}
		#endregion Private Event Handlers

		private void tablesTree_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.Source is TreeView)
			{
				bool isValid = this.ColumnsGrid.IsValid();

				if (isValid == false)
				{
					MessageBox.Show("One of more of the generator options forms are invalid. Please fix and try again.");

					e.Handled = true;
				}
			}
		}
	}
}