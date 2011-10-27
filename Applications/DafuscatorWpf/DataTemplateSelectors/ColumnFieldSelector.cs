using System.Windows;
using System.Windows.Controls;
using WaveTech.Dafuscator.Model;

namespace WaveTech.Dafuscator.WpfApplication.DataTemplateSelectors
{
	public class ColumnFieldSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			Column column = item as Column;
			Window window = Application.Current.MainWindow;

			if (column != null)
			{
				if (column.IsPrimaryKey)
					return window.FindResource("PrimaryKeyColumn") as DataTemplate;

				if (column.IsForignKey)
					return window.FindResource("ForignKeyColumn") as DataTemplate;

				return window.FindResource("NormalColumn") as DataTemplate;

				//switch (column.GeneratorType)
				//{
				//  case GeneratorTypes.None:
				//    return window.FindResource("NormalColumn") as DataTemplate;

				//  default:
				//    return window.FindResource("NormalColumn") as DataTemplate;
				//}
			}

			return base.SelectTemplate(item, container);
		}
	}
}