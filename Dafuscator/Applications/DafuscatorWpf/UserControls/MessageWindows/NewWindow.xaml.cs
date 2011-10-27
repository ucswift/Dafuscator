using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WaveTech.Dafuscator.Model;

namespace WaveTech.Dafuscator.WpfApplication.UserControls.MessageWindows
{
	/// <summary>
	/// Interaction logic for NewWindow.xaml
	/// </summary>
	public partial class NewWindow : UserControl
	{
		#region Private Members
		private Brush originalBrush;
		#endregion Private Members

		public bool IsUpdateOnly { get; set; }

		#region Constructor
		public NewWindow()
		{
			InitializeComponent();

			ResetForm();
		}
		#endregion Constructor

		#region Routed Events
		public static readonly RoutedEvent ConnectButtonClickEvent = EventManager.RegisterRoutedEvent(
		"ConnectButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NewWindow));


		public event RoutedEventHandler ConnectButtonClick
		{
			add { AddHandler(ConnectButtonClickEvent, value); }
			remove { RemoveHandler(ConnectButtonClickEvent, value); }
		}

		public static readonly RoutedEvent CancelButtonClickEvent = EventManager.RegisterRoutedEvent(
		"CancelButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NewWindow));


		public event RoutedEventHandler CancelButtonClick
		{
			add { AddHandler(CancelButtonClickEvent, value); }
			remove { RemoveHandler(CancelButtonClickEvent, value); }
		}
		#endregion Routed Events

		#region Private Event Handelers
		private void btnConnect_Click(object sender, RoutedEventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(ConnectButtonClickEvent));
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(CancelButtonClickEvent));
		}

		private void cboAuthenticationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cboAuthenticationType.SelectedIndex == 0)
			{
				DisableFormForSqlAuth();
			}
			else
			{
				EnableFormForSqlAuth();
			}
		}
		#endregion Private Event Handelers

		#region Private Methods
		private void EnableFormForSqlAuth()
		{
			txtUserName.IsEnabled = true;
			txtUserName.Background = Brushes.White;

			txtPassword.IsEnabled = true;
			txtPassword.Background = Brushes.White;
		}

		private void DisableFormForSqlAuth()
		{
			txtUserName.Text = String.Empty;
			txtUserName.IsEnabled = false;
			txtUserName.Background = Brushes.DarkGray;

			txtPassword.Password = String.Empty;
			txtPassword.IsEnabled = false;
			txtPassword.Background = Brushes.DarkGray;
		}
		#endregion Private Methods

		#region Public Methods
		public void ResetForm()
		{
			txtServerName.Text = String.Empty;
			txtDatabaseName.Text = String.Empty;
			txtUserName.Text = String.Empty;
			txtPassword.Password = String.Empty;

			DisableFormForSqlAuth();

			cboAuthenticationType.SelectedIndex = 0;
		}

		public ConnectionString GetConnectionString()
		{
			if (cboAuthenticationType.SelectedIndex == 0)
				return new ConnectionString(txtServerName.Text, txtDatabaseName.Text);
			else
				return new ConnectionString(txtServerName.Text, txtDatabaseName.Text, txtUserName.Text, txtPassword.Password);
		}

		public bool IsFormValid()
		{
			bool isValid = true;

			if (originalBrush == null)
				originalBrush = txtDatabaseName.BorderBrush;

			if (String.IsNullOrEmpty(txtDatabaseName.Text))
			{
				txtDatabaseName.BorderBrush = Brushes.Red;
				isValid = false;
			}
			else
			{
				txtDatabaseName.BorderBrush = originalBrush;
			}

			if (String.IsNullOrEmpty(txtServerName.Text))
			{
				txtServerName.BorderBrush = Brushes.Red;
				isValid = false;
			}
			else
			{
				txtServerName.BorderBrush = originalBrush;
			}

			if (cboAuthenticationType.SelectedIndex == 1)
			{
				if (String.IsNullOrEmpty(txtUserName.Text))
				{
					txtUserName.BorderBrush = Brushes.Red;
					isValid = false;
				}
				else
				{
					txtUserName.BorderBrush = originalBrush;
				}

				if (String.IsNullOrEmpty(txtPassword.Password))
				{
					txtPassword.BorderBrush = Brushes.Red;
					isValid = false;
				}
				else
				{
					txtPassword.BorderBrush = originalBrush;
				}
			}

			return isValid;
		}
		#endregion Public Methods
	}
}