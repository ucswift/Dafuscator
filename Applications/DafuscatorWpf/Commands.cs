using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Services;
using WaveTech.Dafuscator.WpfApplication.Classes;
using WaveTech.Dafuscator.WpfApplication.UserControls.MessageWindows;

namespace WaveTech.Dafuscator.WpfApplication
{
	public class Commands
	{
		#region Command Routers
		public static readonly RoutedUICommand SaveCommand = new RoutedUICommand("Save", "SaveCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S") }));

		public static readonly RoutedUICommand NewCommand = new RoutedUICommand("New", "NewCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N") }));

		public static readonly RoutedUICommand OpenCommand = new RoutedUICommand("Open", "OpenCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl+O") }));

		public static readonly RoutedUICommand TestCommand = new RoutedUICommand("Test", "TestCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.T, ModifierKeys.Control, "Ctrl+T") }));

		public static readonly RoutedUICommand RunCommand = new RoutedUICommand("Run", "RunCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R") }));

		public static readonly RoutedUICommand ExportAllCommand = new RoutedUICommand("ExportAll", "ExportAllCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E") }));

		public static readonly RoutedUICommand ExportTableCommand = new RoutedUICommand("ExportTable", "ExportTableCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+T") }));

		public static readonly RoutedUICommand HomeCommand = new RoutedUICommand("Home", "HomeCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+H") }));

		public static readonly RoutedUICommand ReportCommand = new RoutedUICommand("Report", "ReportCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+R") }));

		public static readonly RoutedUICommand ExportAllTestCommand = new RoutedUICommand("ExportAllTest", "ExportAllTestCommand", typeof(MainWindow),
		new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+Y") }));

		public static readonly RoutedUICommand ExportTableTestCommand = new RoutedUICommand("ExportTableTest", "ExportTableTestCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+I") }));

		public static readonly RoutedUICommand RefreshCommand = new RoutedUICommand("Refresh", "RefreshCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+U") }));

		public static readonly RoutedUICommand UpdateConnectionCommand = new RoutedUICommand("UpdateConnection", "UpdateConnectionCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+UC") }));

		public static readonly RoutedUICommand HelpCommand = new RoutedUICommand("Help", "HelpCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+D") }));

		public static readonly RoutedUICommand AboutCommand = new RoutedUICommand("About", "AboutCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+A") }));

		public static readonly RoutedUICommand PreviewCommand = new RoutedUICommand("Preview", "PreviewCommand", typeof(MainWindow),
				new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+P") }));

		public static readonly RoutedUICommand RunTableCommand = new RoutedUICommand("RunTable", "RunTableCommand", typeof(MainWindow),
		new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+O") }));

		public static readonly RoutedUICommand CloseCommand = new RoutedUICommand("Close", "CloseCommand", typeof(MainWindow),
		new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+C") }));
		#endregion Command Routers

		#region Constructor
		static Commands()
		{
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(SaveCommand, SaveProject));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NewCommand, NewProject));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(OpenCommand, OpenProject));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(TestCommand, TestObfuscate));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(RunCommand, RunObfuscate));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(ExportAllCommand, ExportAllObfuscate));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(ExportTableCommand, ExportTableObfuscate));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(HomeCommand, Home));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(ReportCommand, Report));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(ExportAllTestCommand, ExportAllTestObfuscate));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(ExportTableTestCommand, ExportTableTestObfuscate));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(RefreshCommand, Refresh));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(UpdateConnectionCommand, UpdateConnection));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(HelpCommand, Help));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(AboutCommand, About));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(PreviewCommand, Preview));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(RunTableCommand, RunTable));
			CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(CloseCommand, Close));
		}
		#endregion Constructor

		#region Private Event Handlers
		private static void SaveProject(object sender, ExecutedRoutedEventArgs e)
		{
			if (UIContext.Database != null)
			{
				BackgroundWorker worker = new BackgroundWorker();

				SaveFileDialog dialog = new SaveFileDialog();
				dialog.DefaultExt = ".daf";
				dialog.Filter = "Dafsucator Projects (.daf)|*.daf";

				bool? result = dialog.ShowDialog();
				if (result == true)
				{
					MainWindow mainWindow = (MainWindow)sender;
					mainWindow.loadingAnimation.Visibility = Visibility.Visible;

					worker.DoWork += delegate(object s, DoWorkEventArgs args)
					{
						IDatabaseProjectService databaseProjectService = ObjectLocator.GetInstance<IDatabaseProjectService>();
						string filePath = args.Argument as string;

						databaseProjectService.SaveDatabaseProject(UIContext.Database, filePath);
					};

					worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
					{

						mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
					};

					worker.RunWorkerAsync(dialog.FileName);
				}
			}
			else
			{
				MessageBox.Show("There is no open database project, cannot save.", "Error Saving", MessageBoxButton.OK,
												MessageBoxImage.Exclamation);
			}
		}

		private static void NewProject(object sender, ExecutedRoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)sender;
			mainWindow.newWindow.IsUpdateOnly = false;
			mainWindow.newWindow.Visibility = Visibility.Visible;
		}

		private static void Close(object sender, ExecutedRoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)sender;
			UIContext.Database = null;

			mainWindow.RefreshData();
			mainWindow.ColumnsGrid.Visibility = Visibility.Collapsed;
		}

		private static void OpenProject(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = ".daf";
			dialog.Filter = "Dafsucator Projects (.daf)|*.daf";

			bool? result = dialog.ShowDialog();
			if (result == true)
			{
				string filePath = dialog.FileName;

				IDatabaseProjectService databaseProjectService = ObjectLocator.GetInstance<IDatabaseProjectService>();

				Database db = databaseProjectService.GetDatabaseProject(filePath);
				UIContext.Database = db;

				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.RefreshData();
				mainWindow.ColumnsGrid.Visibility = Visibility.Visible;
			}
		}

		private static void TestObfuscate(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void RunObfuscate(object sender, ExecutedRoutedEventArgs e)
		{
			if (UIContext.Database != null)
			{
				BackgroundWorker worker = new BackgroundWorker();

				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					IRunService runService = ObjectLocator.GetInstance<IRunService>();

					ObfuscationResult result = runService.ObfuscateDatabase(UIContext.Database);

					args.Result = result;
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
					ObfuscationResult result = args.Result as ObfuscationResult;

					MessageBox.Show(mainWindow, string.Format("Finished database obfuscation in {0}", result.TimeElapsed), "Database Obfuscation Finished",
						MessageBoxButton.OK, MessageBoxImage.Information);

					IReportService reportService = ObjectLocator.GetInstance<IReportService>();

					//string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
					//path = path.Replace("file:\\", "");

					string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					path = path + "\\Dafuscator";

					if (Directory.Exists(path) == false)
						Directory.CreateDirectory(path);

					path = path +
								 string.Format("\\DatabaseObfuscationReport_{0}_{1}_{2}-{3}_{4}.txt", DateTime.Now.Month, DateTime.Now.Day,
															 DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);

					reportService.GenerateReportForObfucsationResult(result, path);
				};

				worker.RunWorkerAsync();
			}
			else
			{
				MessageBox.Show("There is no open database project, cannot obfuscate database.", "Error Saving", MessageBoxButton.OK,
												MessageBoxImage.Exclamation);
			}
		}

		private static void Report(object sender, ExecutedRoutedEventArgs e)
		{
			if (UIContext.Database != null)
			{
				BackgroundWorker worker = new BackgroundWorker();

				SaveFileDialog dialog = new SaveFileDialog();
				dialog.DefaultExt = ".txt";
				dialog.Filter = "Text Documents (.txt)|*.txt";

				bool? result = dialog.ShowDialog();
				if (result == true)
				{
					MainWindow mainWindow = (MainWindow)sender;
					mainWindow.loadingAnimation.Visibility = Visibility.Visible;

					worker.DoWork += delegate(object s, DoWorkEventArgs args)
					{
						IReportService reportService = ObjectLocator.GetInstance<IReportService>();
						string filePath = args.Argument as string;

						reportService.GenerateObfuscationReportForDatabase(UIContext.Database, filePath);
						args.Result = filePath;
					};

					worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
					{
						mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;

						if (args.Result != null && !String.IsNullOrEmpty(args.Result.ToString()))
							System.Diagnostics.Process.Start("notepad.exe", args.Result.ToString());
					};

					worker.RunWorkerAsync(dialog.FileName);
				}
			}
			else
			{
				MessageBox.Show("There is no open database project, cannot generate report.", "Error Saving", MessageBoxButton.OK,
												MessageBoxImage.Exclamation);
			}
		}

		private static void ExportAllObfuscate(object sender, ExecutedRoutedEventArgs e)
		{
			BackgroundWorker worker = new BackgroundWorker();

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".sql";
			dialog.Filter = "SQL Files (.sql)|*.sql";

			bool? result = dialog.ShowDialog();
			if (result == true)
			{
				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					IExportService exportService = ObjectLocator.GetInstance<IExportService>();
					string filePath = args.Argument as string;

					exportService.ExportTables(UIContext.Database.Tables, filePath, UIContext.Database.ConnectionString);
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
				};

				worker.RunWorkerAsync(dialog.FileName);
			}
		}

		private static void ExportTableObfuscate(object sender, ExecutedRoutedEventArgs e)
		{
			BackgroundWorker worker = new BackgroundWorker();

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".sql";
			dialog.Filter = "SQL Files (.sql)|*.sql";

			bool? result = dialog.ShowDialog();
			if (result == true)
			{
				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					IExportService exportService = ObjectLocator.GetInstance<IExportService>();
					object[] data = args.Argument as object[];

					exportService.ExportTable((Table)data[1], data[0].ToString(), UIContext.Database.ConnectionString);
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
				};

				worker.RunWorkerAsync(new object[]
				{
					dialog.FileName,
					mainWindow.ColumnsGrid.SelectedTable
				});
			}
		}

		private static void ExportAllTestObfuscate(object sender, ExecutedRoutedEventArgs e)
		{
			BackgroundWorker worker = new BackgroundWorker();

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".sql";
			dialog.Filter = "SQL Files (.sql)|*.sql";

			bool? result = dialog.ShowDialog();
			if (result == true)
			{
				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					IExportService exportService = ObjectLocator.GetInstance<IExportService>();
					string filePath = args.Argument as string;

					exportService.ExportTestTables(UIContext.Database.Tables, filePath, UIContext.Database.ConnectionString);
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
				};

				worker.RunWorkerAsync(dialog.FileName);
			}
		}

		private static void ExportTableTestObfuscate(object sender, ExecutedRoutedEventArgs e)
		{
			BackgroundWorker worker = new BackgroundWorker();

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".sql";
			dialog.Filter = "SQL Files (.sql)|*.sql";

			bool? result = dialog.ShowDialog();
			if (result == true)
			{
				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					IExportService exportService = ObjectLocator.GetInstance<IExportService>();
					object[] data = args.Argument as object[];

					exportService.ExportTestTable((Table)data[1], data[0].ToString(), UIContext.Database.ConnectionString);
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
				};

				worker.RunWorkerAsync(new object[]
				{
					dialog.FileName,
					mainWindow.ColumnsGrid.SelectedTable
				});
			}
		}

		private static void Home(object sender, ExecutedRoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.dafuscator.com");
		}

		private static void About(object sender, ExecutedRoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)sender;

			AboutBox about = new AboutBox(mainWindow);
			about.ShowDialog();
		}

		private static void Help(object sender, ExecutedRoutedEventArgs e)
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			path = path.Replace("file:\\", "");

			System.Diagnostics.Process.Start(string.Format("{0}\\DafuscatorDocumentation.chm", path));
		}

		private static void Preview(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private static void Refresh(object sender, ExecutedRoutedEventArgs e)
		{
			if (UIContext.Database != null)
			{
				BackgroundWorker worker = new BackgroundWorker();

				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					IRefreshService refreshService = ObjectLocator.GetInstance<IRefreshService>();

					UIContext.Database = refreshService.RefreshDatabaseProject(UIContext.Database);

					mainWindow.RefreshData();
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
				};

				worker.RunWorkerAsync();
			}
			else
			{
				MessageBox.Show("There is no open database project, cannot generate report.", "Error Saving", MessageBoxButton.OK,
												MessageBoxImage.Exclamation);
			}

		}

		private static void UpdateConnection(object sender, ExecutedRoutedEventArgs e)
		{
			if (UIContext.Database != null)
			{
				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.newWindow.IsUpdateOnly = true;
				mainWindow.newWindow.Visibility = Visibility.Visible;
			}
			else
			{
				MessageBox.Show("There is no open database project, cannot update connection string.", "Error Saving", MessageBoxButton.OK,
												MessageBoxImage.Exclamation);
			}
		}

		private static void RunTable(object sender, ExecutedRoutedEventArgs e)
		{
			if (UIContext.Database != null)
			{
				BackgroundWorker worker = new BackgroundWorker();
				MainWindow mainWindow = (MainWindow)sender;
				mainWindow.loadingAnimation.Visibility = Visibility.Visible;

				worker.DoWork += delegate(object s, DoWorkEventArgs args)
				{
					object[] data = args.Argument as object[];
					IRunService runService = ObjectLocator.GetInstance<IRunService>();

					ObfuscationResult result = runService.ObfuscateTable(UIContext.Database.ConnectionString, data[0] as Table);

					args.Result = result;
				};

				worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
				{
					mainWindow.loadingAnimation.Visibility = Visibility.Collapsed;
					ObfuscationResult result = args.Result as ObfuscationResult;

					MessageBox.Show(mainWindow, string.Format("Finished table obfuscation in {0}", result.TimeElapsed), "Table Obfuscation Finished",
						MessageBoxButton.OK, MessageBoxImage.Information);

					IReportService reportService = ObjectLocator.GetInstance<IReportService>();

					//string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
					//path = path.Replace("file:\\", "");

					string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					path = path + "\\Dafuscator";

					if (Directory.Exists(path) == false)
						Directory.CreateDirectory(path);

					path = path +
								 string.Format("\\TableObfuscationReport_{0}_{1}_{2}-{3}_{4}.txt", DateTime.Now.Month, DateTime.Now.Day,
															 DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);

					reportService.GenerateReportForObfucsationResult(result, path);
				};

				worker.RunWorkerAsync(new object[]
				{
					mainWindow.ColumnsGrid.SelectedTable
				});
			}
			else
			{
				MessageBox.Show("There is no open database project, cannot obfuscate database.", "Error Saving", MessageBoxButton.OK,
												MessageBoxImage.Exclamation);
			}
		}
		#endregion Private Event Handlers
	}
}