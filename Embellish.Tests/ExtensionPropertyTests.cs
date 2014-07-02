
using System;
using NUnit.Framework;
using Embellish.ExtensionProperties;

namespace Embellish.Tests
{

	[TestFixture]
	public class ExtensionPropertyTests
	{
		
		[Test]
		public void SimpleTest()
		{
			var a = new object();
			var b = new object();
			
			a.SetExtensionProperty("name", "five");
			b.SetExtensionProperty("name", "three");
			
			Assert.That(a.GetExtensionProperty("name"), Is.EqualTo("five"));
			Assert.That(b.GetExtensionProperty("name"), Is.EqualTo("three"));
			Assert.That(b.GetGenericExtensionProperty<string>("name"), Is.EqualTo("three"));
		}
	}
}
