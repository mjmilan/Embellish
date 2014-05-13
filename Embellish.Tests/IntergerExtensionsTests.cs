/*
 * Created by SharpDevelop.
 * User: Martin
 * Date: 13/05/2014
 * Time: 00:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Embellish;

namespace Embellish.Tests
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[TestFixture]
	public class IntegerExtensionsTests
	{
		[Test]
		public void TimesMethodRepeatsActionNTimes()
		{
			int i = 0;
			5.Times(() => i++);
			Assert.That(i, Is.EqualTo(5));
		}
	}
}