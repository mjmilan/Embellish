/*
 * User: Martin Milan
 * Date: 01/07/2015
 */
using System;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
namespace Embellish.XMLSubstitution
{
	/// <summary>
	/// Description of XMLElementSubstitution.
	/// </summary>
	public class XMLElementSubstitution : BaseClasses.XMLSubstitiutionBase
	{
		#region Properties
		public object InformationSource{get; set;}
		#endregion
		#region Constructors
		public XMLElementSubstitution() : base()
		{
		}
		
		public XMLElementSubstitution(XName nameOfSubstitutionElement, XDocument xmlDoc) : base(nameOfSubstitutionElement, xmlDoc)
		{
		
		}
		#endregion
		
		#region Methods
		public override void SubstituteMatch(XElement match)
		{
			if (this.InformationSource != null)
			{
				string content = match.Value;
				var sourceType = this.InformationSource.GetType();
				var methodInfo = sourceType.GetMethod(content, BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance);
				var result = methodInfo.Invoke(this.InformationSource,null) as XElement;
				if (result != null)
				{
					match.AddBeforeSelf(result);
				}
				
				match.Remove();
			}
		}
		#endregion
	}
}
