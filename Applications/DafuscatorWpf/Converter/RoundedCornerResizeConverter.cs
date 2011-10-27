using System;
using System.Windows;
using System.Windows.Data;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	[ValueConversion(typeof(double), typeof(CornerRadius))]
	public class RoundedCornerResizeConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			double d = parameter != null ? double.Parse(parameter.ToString()) : -1;
			CornerRadius r = (CornerRadius)value;
			CornerRadius result = new CornerRadius(
					Math.Max(0, r.TopLeft + d),
					Math.Max(0, r.TopRight + d),
					Math.Max(0, r.BottomRight + d),
					Math.Max(0, r.BottomLeft + d));

			return result;

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}