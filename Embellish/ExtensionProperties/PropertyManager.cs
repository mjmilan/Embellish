/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 20/06/2014
 * Time: 00:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace Embellish.ExtensionProperties
{
	/// <summary>
	/// Description of PropertyManager.
	/// </summary>
	internal class PropertyManager
	{
		#region Members
		protected object _host = null;
		protected ExtensionPropertiesAttribute _epa = null;
		#endregion
		
		#region Constructor
		internal PropertyManager(object host)
		{
			_host = host;
			bool found = false;
			foreach(var attr in TypeDescriptor.GetAttributes(_host))
			{
				var epa = attr as ExtensionPropertiesAttribute;
				if (epa != null)
				{
					found = true;
					_epa = epa;
					break;
				}
			}
			
			if (!found)
			{
				_epa = new ExtensionPropertiesAttribute();
				TypeDescriptor.AddAttributes(_host, _epa);
			}
		}
		#endregion
		
		#region Methods
		internal void SetPropertyValue(string propertyName, object value, bool supressDispose = false)
		{
			if ((!supressDispose) && IsPropertySupported(propertyName))
			{
				var oldSetting = _epa.PropertyValues[propertyName];
				if (oldSetting != value)
				{
					var disposable = oldSetting as IDisposable;
					if (disposable != null) disposable.Dispose();
				}
			}
			
			if (!IsPropertySupported(propertyName) ||  _epa.PropertyValues[propertyName] != value)
			{
				_epa.PropertyValues[propertyName] = value;
			}
		
		}
		
		internal object GetPropertyValue(string propertyName)
		{
			if (IsPropertySupported(propertyName))
			{
				return _epa.PropertyValues[propertyName];
			}
			else
			{
				throw new Exceptions.ExtensionPropertyNotSupported(string.Format("This object does not support an extension property called {0}", propertyName));
			}
		}
		
		internal void RemovePropertyValue(string propertyName, bool suppressDispose = false)
		{
			if (IsPropertySupported(propertyName) && (!suppressDispose))
			{
				var disposable = GetPropertyValue(propertyName) as IDisposable;
				if (disposable != null) disposable.Dispose();
			}
			
			_epa.PropertyValues.Remove(propertyName);
		}
		
		internal bool IsPropertySupported(string propertyName)
		{
			bool result = false;
			if (_epa != null)
			{
				result = _epa.PropertyValues.ContainsKey(propertyName);
			}
			return result;
		}
		
		
		#endregion
	}
}
