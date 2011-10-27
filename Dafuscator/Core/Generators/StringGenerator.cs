using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class StringGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F"); }
		}

		public string Name
		{
			get { return "Random String Generator"; }
		}

		public Type Type
		{
			get { return typeof(IStringGenerator); }
		}

		public List<OleDbType> CompatibleDataTypes
		{
			get
			{
				return new List<OleDbType>
								{
									OleDbType.LongVarChar,
									OleDbType.LongVarWChar,
									OleDbType.VarChar,
									OleDbType.VarWChar
								};
			}
		}
	}

	public class StringGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				bool stringBool1 = false;

				try
				{
					stringBool1 = (bool)data[3];
				}
				catch { }

				bool stringBool2 = false;

				try
				{
					stringBool2 = (bool)data[4];
				}
				catch { }

				generatedData = ((IStringGenerator)generator).GenerateRandomStrings(double.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()), stringBool1, stringBool2, existingColumnData);

			}

			return generatedData;
		}
	}

	public class StringGenerator : IStringGenerator
	{
		private static string CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
		private static string CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
		private static string CHARS_NUMERIC = "23456789";
		private static string CHARS_SPECIAL = "*$-+?_&=!%{}/";

		public string GenerateRandomString(int minLength, int maxLength, bool includeNumbers, bool includeSpecialCharacters)
		{
			// Code below from: http://www.obviex.com/Samples/Password.aspx


			// Make sure that input parameters are valid.
			if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
				return null;

			// Create a local array containing supported password characters
			// grouped by types. You can remove character groups from this
			// array, but doing so will weaken the password strength.
			char[][] charGroups = null;

			if (includeSpecialCharacters && includeNumbers)
			{
				charGroups = new char[][]
											{
												CHARS_LCASE.ToCharArray(),
												CHARS_UCASE.ToCharArray(),
												CHARS_NUMERIC.ToCharArray(),
												CHARS_SPECIAL.ToCharArray()
											};
			}
			else if (includeSpecialCharacters == false && includeNumbers)
			{
				charGroups = new char[][]
											{
												CHARS_LCASE.ToCharArray(),
												CHARS_UCASE.ToCharArray(),
												CHARS_NUMERIC.ToCharArray()
											};
			}
			else if (includeSpecialCharacters && includeNumbers == false)
			{
				charGroups = new char[][]
											{
												CHARS_LCASE.ToCharArray(),
												CHARS_UCASE.ToCharArray(),
												CHARS_SPECIAL.ToCharArray()
											};
			}
			else if (!includeSpecialCharacters && !includeNumbers)
			{
				charGroups = new char[][]
											{
												CHARS_LCASE.ToCharArray(),
												CHARS_UCASE.ToCharArray()
											};
			}

			// Use this array to track the number of unused characters in each
			// character group.
			int[] charsLeftInGroup = new int[charGroups.Length];

			// Initially, all characters in each group are not used.
			for (int i = 0; i < charsLeftInGroup.Length; i++)
				charsLeftInGroup[i] = charGroups[i].Length;

			// Use this array to track (iterate through) unused character groups.
			int[] leftGroupsOrder = new int[charGroups.Length];

			// Initially, all character groups are not used.
			for (int i = 0; i < leftGroupsOrder.Length; i++)
				leftGroupsOrder[i] = i;

			// Because we cannot use the default randomizer, which is based on the
			// current time (it will produce the same "random" number within a
			// second), we will use a random number generator to seed the
			// randomizer.

			// Use a 4-byte array to fill it with random bytes and convert it then
			// to an integer value.
			byte[] randomBytes = new byte[4];

			// Generate 4 random bytes.
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(randomBytes);

			// Convert 4 bytes into a 32-bit integer value.
			int seed = (randomBytes[0] & 0x7f) << 24 |
								 randomBytes[1] << 16 |
								 randomBytes[2] << 8 |
								 randomBytes[3];

			// Now, this is real randomization.
			Random random = new Random(seed);

			// This array will hold password characters.
			char[] password = null;

			// Allocate appropriate memory for the password.
			if (minLength < maxLength)
				password = new char[random.Next(minLength, maxLength + 1)];
			else
				password = new char[minLength];

			// Index of the next character to be added to password.
			int nextCharIdx;

			// Index of the next character group to be processed.
			int nextGroupIdx;

			// Index which will be used to track not processed character groups.
			int nextLeftGroupsOrderIdx;

			// Index of the last non-processed character in a group.
			int lastCharIdx;

			// Index of the last non-processed group.
			int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

			// Generate password characters one at a time.
			for (int i = 0; i < password.Length; i++)
			{
				// If only one character group remained unprocessed, process it;
				// otherwise, pick a random character group from the unprocessed
				// group list. To allow a special character to appear in the
				// first position, increment the second parameter of the Next
				// function call by one, i.e. lastLeftGroupsOrderIdx + 1.
				if (lastLeftGroupsOrderIdx == 0)
					nextLeftGroupsOrderIdx = 0;
				else
					nextLeftGroupsOrderIdx = random.Next(0,
																							 lastLeftGroupsOrderIdx);

				// Get the actual index of the character group, from which we will
				// pick the next character.
				nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

				// Get the index of the last unprocessed characters in this group.
				lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

				// If only one unprocessed character is left, pick it; otherwise,
				// get a random character from the unused character list.
				if (lastCharIdx == 0)
					nextCharIdx = 0;
				else
					nextCharIdx = random.Next(0, lastCharIdx + 1);

				// Add this character to the password.
				password[i] = charGroups[nextGroupIdx][nextCharIdx];

				// If we processed the last character in this group, start over.
				if (lastCharIdx == 0)
					charsLeftInGroup[nextGroupIdx] =
						charGroups[nextGroupIdx].Length;
				// There are more unprocessed characters left.
				else
				{
					// Swap processed character with the last unprocessed character
					// so that we don't pick it until we process all characters in
					// this group.
					if (lastCharIdx != nextCharIdx)
					{
						char temp = charGroups[nextGroupIdx][lastCharIdx];
						charGroups[nextGroupIdx][lastCharIdx] =
							charGroups[nextGroupIdx][nextCharIdx];
						charGroups[nextGroupIdx][nextCharIdx] = temp;
					}
					// Decrement the number of unprocessed characters in
					// this group.
					charsLeftInGroup[nextGroupIdx]--;
				}

				// If we processed the last group, start all over.
				if (lastLeftGroupsOrderIdx == 0)
					lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
				// There are more unprocessed groups left.
				else
				{
					// Swap processed group with the last unprocessed group
					// so that we don't pick it until we process all groups.
					if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
					{
						int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
						leftGroupsOrder[lastLeftGroupsOrderIdx] =
							leftGroupsOrder[nextLeftGroupsOrderIdx];
						leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
					}
					// Decrement the number of unprocessed groups.
					lastLeftGroupsOrderIdx--;
				}
			}

			// Convert password characters into a string and return the result.
			return new string(password);
		}

		public List<string> GenerateRandomStrings(double count, int minLength, int maxLength, bool includeSpecialCharacters, bool includeNumbers, HashSet<string> existingColumnData)
		{
			HashSet<string> strings = new HashSet<string>();

			while (strings.Count < count)
			{
				string randomString = GenerateRandomString(minLength, maxLength, includeSpecialCharacters, includeNumbers);

				if (strings.Contains(randomString, new IgnoreCaseStringComparer()) == false &&
							existingColumnData.Contains(randomString, new IgnoreCaseStringComparer()) == false)
				{
					strings.Add(randomString);
				}
			}

			return strings.ToList();
		}


		private static string NormalizeName(string name)
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

		private class IgnoreCaseStringComparer : IEqualityComparer<string>
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
