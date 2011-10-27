using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveTech.Dafuscator.Generators.Data
{
	public class CharacterNumberMap
	{
		public static Dictionary<int, char> Map = new Dictionary<int, char>
		{
			{ 0, char.Parse("0")},
			{ 1, char.Parse("1")},
			{ 2, char.Parse("2")},
			{ 3, char.Parse("3")},
			{ 4, char.Parse("4")},
			{ 5, char.Parse("5")},
			{ 6, char.Parse("6")},
			{ 7, char.Parse("7")},
			{ 8, char.Parse("8")},
			{ 9, char.Parse("9")},
			{ 10, char.Parse("A")},
			{ 11, char.Parse("B")},
			{ 12, char.Parse("C")},
			{ 13, char.Parse("D")},
			{ 14, char.Parse("E")},
			{ 15, char.Parse("F")},
			{ 16, char.Parse("G")},
			{ 17, char.Parse("H")},
			{ 18, char.Parse("I")},
			{ 19, char.Parse("J")},
			{ 20, char.Parse("K")},
			{ 21, char.Parse("L")},
			{ 22, char.Parse("M")},
			{ 23, char.Parse("N")},
			{ 24, char.Parse("O")},
			{ 25, char.Parse("P")},
			{ 26, char.Parse("Q")},
			{ 27, char.Parse("R")},
			{ 28, char.Parse("S")},
			{ 29, char.Parse("T")},
			{ 30, char.Parse("U")},
			{ 31, char.Parse("V")},
			{ 32, char.Parse("W")},
			{ 33, char.Parse("X")},
			{ 34, char.Parse("Y")},
			{ 35, char.Parse("Z")}
		};

		public static Dictionary<char, int> ReverseMap = new Dictionary<char, int>
		{
			{ char.Parse("0"), 0},
			{ char.Parse("1"), 1},
			{ char.Parse("2"), 2},
			{ char.Parse("3"), 3},
			{ char.Parse("4"), 4},
			{ char.Parse("5"), 5},
			{ char.Parse("6"), 6},
			{ char.Parse("7"), 7},
			{ char.Parse("8"), 8},
			{ char.Parse("9"), 9},
			{ char.Parse("A"), 10},
			{ char.Parse("B"), 11},
			{ char.Parse("C"), 12},
			{ char.Parse("D"), 13},
			{ char.Parse("E"), 14},
			{ char.Parse("F"), 15},
			{ char.Parse("G"), 16},
			{ char.Parse("H"), 17},
			{ char.Parse("I"), 18},
			{ char.Parse("J"), 19},
			{ char.Parse("K"), 20},
			{ char.Parse("L"), 21},
			{ char.Parse("M"), 22},
			{ char.Parse("N"), 23},
			{ char.Parse("O"), 24},
			{ char.Parse("P"), 25},
			{ char.Parse("Q"), 26},
			{ char.Parse("R"), 27},
			{ char.Parse("S"), 28},
			{ char.Parse("T"), 29},
			{ char.Parse("U"), 30},
			{ char.Parse("V"), 31},
			{ char.Parse("W"), 32},
			{ char.Parse("X"), 33},
			{ char.Parse("Y"), 34},
			{ char.Parse("Z"), 35}
		};
	}

	public class CharacterOnlyMap
	{
		public static Dictionary<int, char> Map = new Dictionary<int, char>
		{
			{ 0, char.Parse("A")},
			{ 1, char.Parse("B")},
			{ 2, char.Parse("C")},
			{ 3, char.Parse("D")},
			{ 4, char.Parse("E")},
			{ 5, char.Parse("F")},
			{ 6, char.Parse("G")},
			{ 7, char.Parse("H")},
			{ 8, char.Parse("I")},
			{ 9, char.Parse("J")},
			{ 10, char.Parse("K")},
			{ 11, char.Parse("L")},
			{ 12, char.Parse("M")},
			{ 13, char.Parse("N")},
			{ 14, char.Parse("O")},
			{ 15, char.Parse("P")},
			{ 16, char.Parse("Q")},
			{ 17, char.Parse("R")},
			{ 18, char.Parse("S")},
			{ 19, char.Parse("T")},
			{ 20, char.Parse("U")},
			{ 21, char.Parse("V")},
			{ 22, char.Parse("W")},
			{ 23, char.Parse("X")},
			{ 24, char.Parse("Y")},
			{ 25, char.Parse("Z")}
		};

		public static Dictionary<char, int> ReverseMap = new Dictionary<char, int>
		{
			{ char.Parse("A"), 0},
			{ char.Parse("B"), 1},
			{ char.Parse("C"), 2},
			{ char.Parse("D"), 3},
			{ char.Parse("E"), 4},
			{ char.Parse("F"), 5},
			{ char.Parse("G"), 6},
			{ char.Parse("H"), 7},
			{ char.Parse("I"), 8},
			{ char.Parse("J"), 9},
			{ char.Parse("K"), 10},
			{ char.Parse("L"), 11},
			{ char.Parse("M"), 12},
			{ char.Parse("N"), 13},
			{ char.Parse("O"), 14},
			{ char.Parse("P"), 15},
			{ char.Parse("Q"), 16},
			{ char.Parse("R"), 17},
			{ char.Parse("S"), 18},
			{ char.Parse("T"), 19},
			{ char.Parse("U"), 20},
			{ char.Parse("V"), 21},
			{ char.Parse("W"), 22},
			{ char.Parse("X"), 23},
			{ char.Parse("Y"), 24},
			{ char.Parse("Z"), 25}
		};
	}
}