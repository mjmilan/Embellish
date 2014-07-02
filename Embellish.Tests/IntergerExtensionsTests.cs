
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Embellish;

namespace Embellish.Tests
{

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