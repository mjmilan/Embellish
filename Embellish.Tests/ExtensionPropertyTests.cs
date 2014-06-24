/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 20/06/2014
 * Time: 01:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using Embellish.ExtensionProperties;

namespace Embellish.Tests
{
	/// <summary>
	/// Description of ExtensionPropertyTests.
	/// </summary>
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
			
		}
	}
}
