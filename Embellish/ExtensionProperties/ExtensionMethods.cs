/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 20/06/2014
 * Time: 01:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Embellish.ExtensionProperties
{
	/// <summary>
	/// Description of ExtensionMethods.
	/// </summary>
	public static class ExtensionMethods
	{
		public static object GetExtensionProperty(this object host, string propertyName)
		{
			object result = null;
			PropertyManager pm = new PropertyManager(host);
			result = pm.GetPropertyValue(propertyName);
			return result;
		}
		
		public static void SetExtensionProperty(this object host, string propertyName, object value, bool suppressDispose = true)
		{
			var pm = new PropertyManager(host);
			pm.SetPropertyValue(propertyName, value, suppressDispose);
		}
		
		public static bool IsExtensionPropertySupported(this object host, string propertyName)
		{
			var pm = new PropertyManager(host);
			return pm.IsPropertySupported(propertyName);
		}
		
		public static void RemoveExtensionProperty(this object host, string propertyName, bool suppressDispose = true)
		{
			var pm = new PropertyManager(host);
			pm.RemovePropertyValue(propertyName, suppressDispose);
		}
	}
}
