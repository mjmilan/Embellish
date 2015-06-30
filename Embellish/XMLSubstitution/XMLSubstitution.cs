
using System;
using System.Xml.Linq;
using System.Reflection;

namespace Embellish.XMLSubstitution
{
	/// <summary>
	/// Description of XMLSubstitution.
	/// </summary>
	public class XMLSubstitution : BaseClasses.XMLSubstitiutionBase
	{
		#region Properties
		public object InformationSource{get; set;}
		#endregion
		#region Constructors
		public XMLSubstitution() : base()
		{
		}
		
		public XMLSubstitution(XName nameOfSubstitutionElement, XDocument xmlDoc) : base(nameOfSubstitutionElement, xmlDoc)
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
				var result = methodInfo.Invoke(this.InformationSource,null).ToString();
				match.AddBeforeSelf(new XText(result));
				match.Remove();
			}
		}
		#endregion
	}
}
