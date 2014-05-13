/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 13/05/2014
 * Time: 00:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Embellish
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
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