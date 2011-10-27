using System.Windows;
using WaveTech.Dafuscator.WpfApplication;

namespace DafuscatorWpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			Bootstrapper.Configure();

			base.OnStartup(e);
		}
	}
}
