
using System;
using System.Collections.Generic;

namespace Embellish
{

	public static class IntegerExtensions
	{
		#region Extension Methods
		public static void Times(this int occurences, Action action)
		{
			for(int i = 1; i <= occurences; i++)
			{
				action.Invoke();
			}
		}
		#endregion
		
	}
}