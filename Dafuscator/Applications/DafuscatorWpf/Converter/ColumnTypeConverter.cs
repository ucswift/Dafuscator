using System;
using System.Data.OleDb;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	public class ColumnTypeConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			OleDbType type = (OleDbType)value;
			string image = null;

			switch (type)
			{
				case OleDbType.Binary:
					image = "pack://application:,,,/img/Binary_16.png";
					break;
				case OleDbType.Numeric:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.VarNumeric:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.Integer:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.TinyInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.BigInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.SmallInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.UnsignedBigInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.UnsignedInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.UnsignedSmallInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.UnsignedTinyInt:
					image = "pack://application:,,,/img/Number_16.png";
					break;
				case OleDbType.Boolean:
					image = "pack://application:,,,/img/Boolean_16.png";
					break;
				case OleDbType.Date:
					image = "pack://application:,,,/img/Date_16.png";
					break;
				case OleDbType.DBDate:
					image = "pack://application:,,,/img/Date_16.png";
					break;
				case OleDbType.DBTime:
					image = "pack://application:,,,/img/Date_16.png";
					break;
				case OleDbType.DBTimeStamp:
					image = "pack://application:,,,/img/Date_16.png";
					break;
					default:
					image = "pack://application:,,,/img/Text_16.png";
					break;
			}

			return new BitmapImage(new Uri(image));
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}