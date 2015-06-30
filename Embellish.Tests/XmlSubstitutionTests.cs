
using System;
using NUnit.Framework;
using System.Xml.Linq;

namespace Embellish.Tests
{
	/// <summary>
	/// Description of XmlSubstitutionTests.
	/// </summary>
	[TestFixture]
	public class XmlSubstitutionTests
	{
		#region Members
		Embellish.XMLSubstitution.XMLSubstitution _xmlSub;
		
		#endregion
		
		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			//  XName.Get("xml_substitution", "http://www.mjmilan.con/embellish")
			_xmlSub = new Embellish.XMLSubstitution.XMLSubstitution();
		
		}
		
		[Test]
		public void SimpleTest()
		{
			var sb = new System.Text.StringBuilder();
			sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			sb.AppendLine("<doc xmlns:sub=\"http://www.mjmilan.con/embellish\">");
			sb.AppendLine(@"<someInfo>Here I am and my name is <sub:xml_substitution>WhoAmI</sub:xml_substitution></someInfo>");
			sb.AppendLine("</doc>");
			var xmlDoc = XDocument.Parse(sb.ToString());
			var infoSource = new InformationSource();
			
			_xmlSub.InformationSource = infoSource;
			_xmlSub.DocumentToProcess = xmlDoc;
			_xmlSub.MakeSubstitutions();
			
			Assert.That(xmlDoc.ToString(), Contains.Substring("Martin Milan"));
			
			
		}
	}
	
	public class InformationSource 
	{
		public string WhoAmI()
		{
			return "Martin Milan";
		}
	}
	
}
