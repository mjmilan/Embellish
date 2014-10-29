
using System;

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
	}
}
