using System;
using System.Windows.Controls;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Events;
using WaveTech.Dafuscator.Model.Interfaces.Framework;

namespace WaveTech.Dafuscator.WpfApplication.UserControls
{
	/// <summary>
	/// Interaction logic for LoadingAnimationLarge.xaml
	/// </summary>
	public partial class LoadingAnimationLarge : UserControl
	{
		public LoadingAnimationLarge()
		{
			InitializeComponent();

			IEventAggregator eventAggregator = ObjectLocator.GetInstance<IEventAggregator>();
			eventAggregator.AddListener<StatusUpdateEvent>(e => 
																												lblStatus.Dispatcher.Invoke(
																															System.Windows.Threading.DispatcherPriority.Normal,
																																new Action(
																																	delegate()
																																		{
																																			lblStatus.Text = e.Message;
																																		}
																																	)
																																)
																													);
		}
	}
}
