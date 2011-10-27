using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	public class NullableColumnConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool bit = (bool)value;
			string image;

			if (bit)
				image = "pack://application:,,,/img/Nullable_16.png";
			else
				image = "pack://application:,,,/img/NotNullable_16.png";

			return new BitmapImage(new Uri(image));
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}