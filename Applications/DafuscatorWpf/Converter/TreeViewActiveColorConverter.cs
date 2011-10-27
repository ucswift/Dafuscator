using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	public class TreeViewActiveColorConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (parameter != null)
			{
				bool generatorsActive = (bool)parameter;

				if (generatorsActive)
					return Colors.Pink;
			}

			if (value != null)
			{
				if ((bool) value)
					return Colors.Pink;
				else
					return Colors.Gray;
			}

			return Colors.Gray;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
