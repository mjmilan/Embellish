
using System;
using System.Text.RegularExpressions;

namespace Embellish
{
	/// <summary>
	/// This class provides extension methods to instances of the string class.
	/// </summary>
	public static class StringExtensions
	{
		public static string Truncate(this string input, int size)
		{
			if (string.IsNullOrEmpty(input) || input.Length <= size)
			{
				return input;
			}
			else
			{
				return input.Substring(0, size);
			}
			
		}
		
		public static bool ContainsWord(this string input, string word)
		{
			var multipleWords = Regex.Matches(word, "\\b").Count > 2;
			if (multipleWords)
			{
				throw new ArgumentException("You have tried to test against more than one word");
			}
			
			var result = Regex.IsMatch(input, "\\b" + Regex.Escape(word) + "\\b");
			return result;
			
		}
	}
}
