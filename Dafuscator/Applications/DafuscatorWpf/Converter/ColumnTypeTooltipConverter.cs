using System;
using System.Data.OleDb;
using System.Windows.Data;

namespace WaveTech.Dafuscator.WpfApplication.Converter
{
	public class ColumnTypeTooltipConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			OleDbType type = (OleDbType)value;

			switch (type)
			{
				case OleDbType.Binary:
					return "Binary";
				case OleDbType.Numeric:
					return "Numeric";
				case OleDbType.VarNumeric:
					return "Var Numeric";
				case OleDbType.Integer:
					return "Integer";
				case OleDbType.TinyInt:
					return "Tiny Int";
				case OleDbType.BigInt:
					return "Big Int";
				case OleDbType.SmallInt:
					return "Small Int";
				case OleDbType.UnsignedBigInt:
					return "Unsigned Big Int";
				case OleDbType.UnsignedInt:
					return "Unsigned Int";
				case OleDbType.UnsignedSmallInt:
					return "Unsigned Small Int";
				case OleDbType.UnsignedTinyInt:
					return "Unsigned Tiny Int";
				case OleDbType.Boolean:
					return "Boolean";
				case OleDbType.Date:
					return "Date";
				case OleDbType.DBDate:
					return "Date";
				case OleDbType.DBTime:
					return "Time";
				case OleDbType.DBTimeStamp:
					return "Timestamp";
				default:
					return "Text";
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}