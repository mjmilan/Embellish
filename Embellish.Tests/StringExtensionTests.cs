
using System;
using NUnit.Framework;
using Embellish;
namespace Embellish.Tests
{
	[TestFixture]
	public class StringExtensionTests
	{
		[Test]
		public void TruncateNormalUseCase()
		{
			string input = "Hello my lovelies!";
			string output = input.Truncate(5);
			Assert.That(output, Is.EqualTo("Hello"));
		}
		
		[Test]
		public void TruncateWithShortString()
		{
			string input = "Hello";
			string output = input.Truncate(34);
			Assert.That(output, Is.EqualTo(input));
		}
		
		[Test]
		public void TruncateToSameLength()
		{
			string input = "Hello";
			string output = input.Truncate(5);
			Assert.That(output, Is.EqualTo(input));
		}
	
		
	}
}
