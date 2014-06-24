/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 20/06/2014
 * Time: 00:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Embellish.ExtensionProperties
{
	/// <summary>
	/// Description of ExtensionPropertiesAttribute.
	/// </summary>
	internal class ExtensionPropertiesAttribute : Attribute
	{
		
		#region Members
		protected Dictionary<string, object> _propertyValues = new Dictionary<string, object>();
		#endregion
		
		#region Properties
		internal Dictionary<string,object> PropertyValues
		{
			get
			{
				if (_propertyValues == null) _propertyValues = new Dictionary<string, object>();
				return _propertyValues;
			}
		}
		#endregion
		
		#region Constructor
		public ExtensionPropertiesAttribute() : base()
		{
		}
		#endregion
		
		
		
	}
}
