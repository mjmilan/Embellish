
using System;
using System.Xml.Linq;
using System.Linq;

namespace Embellish.BaseClasses
{
	/// <summary>
	/// Base class for functionality for substituting 
	/// </summary>
	public abstract class XMLSubstitiutionBase
	{
		#region Members
		#endregion
		#region Constructors
		public XMLSubstitiutionBase()
		{
			this.NameOfSubstitutionElement = XName.Get("xml_substitution", "http://www.mjmilan.con/embellish");	
		}
		public XMLSubstitiutionBase(XName nameOfSubstitutionElement, XDocument documentToProcess) : this()
		{
			this.NameOfSubstitutionElement = nameOfSubstitutionElement;
			this.DocumentToProcess = documentToProcess;
		}
		#endregion
		#region Properties
		public System.Xml.Linq.XName NameOfSubstitutionElement {get; set;}
		public XDocument DocumentToProcess{get;set;}
		#endregion
		
		#region Methods
		public void MakeSubstitutions()
		{
			if ((this.DocumentToProcess != null) && (this.NameOfSubstitutionElement != null)){
			
				var matches = this.DocumentToProcess.Descendants().Where(x => x.Name.Equals(this.NameOfSubstitutionElement)).ToList();
				foreach (var match in matches)
				{
					SubstituteMatch(match);
				}
			}
			
		}
		
		public abstract void SubstituteMatch(XElement match);
		
		#endregion
	}
}
