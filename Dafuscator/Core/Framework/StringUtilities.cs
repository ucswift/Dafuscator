using System;
using System.Collections.Generic;
using System.Globalization;

namespace WaveTech.Dafuscator.Framework
{
	public class StringUtilities
	{
		public static string NormalizeName(string name)
		{
			string inputString = name.ToLower();
			inputString = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString);
			int indexOfMc = inputString.IndexOf(" Mc");

			//if (indexOfMc > 0)
			//{
			//  inputString.Substring(0, indexOfMc + 3) + inputString[indexOfMc + 3].ToString().ToUpper() + inputString.Substring(indexOfMc + 4);
			//}

			return inputString;
		}

		public class IgnoreCaseStringComparer : IEqualityComparer<string>
		{
			public bool Equals(string x, string y)
			{
				return x.Equals(y, StringComparison.OrdinalIgnoreCase);
			}

			public int GetHashCode(string obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}