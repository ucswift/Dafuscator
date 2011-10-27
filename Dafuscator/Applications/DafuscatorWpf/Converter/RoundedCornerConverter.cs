using System;
using System.Windows;
using System.Windows.Data;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	[ValueConversion(typeof(CornerRadius), typeof(CornerRadius))]
	class RoundedCornerConverter : IValueConverter
	{
		#region IValueConverter Members

		/// <summary>
		/// Converts a RoundedCorner property to an appropriate value.
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			CornerRadius cr = (CornerRadius)value;
			string s = parameter as string;
			switch (s)
			{
				case "left":
					return new CornerRadius(cr.TopLeft, 0d, 0d, cr.BottomLeft);

				case "right":
					return new CornerRadius(0d, cr.TopRight, cr.BottomRight, 0d);

				case "top":
					return new CornerRadius(cr.TopLeft, cr.TopRight, 0d, 0d);

				case "bottom":
					return new CornerRadius(0d, 0d, cr.BottomRight, cr.BottomLeft);

				default:
					return null;
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
