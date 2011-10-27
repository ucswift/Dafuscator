using System;
using System.Windows.Data;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	public class NullableColumnTooltipConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool bit = (bool)value;

			if (bit)
				return "Column is Nullable";
			else
				return "Column is Not Nullable";

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
