using System;
using System.Windows.Data;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	class NullToBoolConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) return false;
			if (String.IsNullOrEmpty(value.ToString())) return false;

			return true;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) return false;

			return (bool)value;
		}

		#endregion
	}
}