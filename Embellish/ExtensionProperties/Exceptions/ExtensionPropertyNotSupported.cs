/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 20/06/2014
 * Time: 01:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Embellish.ExtensionProperties.Exceptions
{
	/// <summary>
	/// Description of ExtensionPropertyNotSupported.
	/// </summary>
	public class ExtensionPropertyNotSupported : Exception
	{
		public ExtensionPropertyNotSupported(string message) : base(message)
		{
		}
	}
}
