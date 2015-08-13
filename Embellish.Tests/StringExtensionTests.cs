
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
	
		[Test]
		[ExpectedException("System.ArgumentException")]
		public void ContainsWordErrorsIfMultipleWordsAreSupplied()
		{
			var result = "Mary had a little".ContainsWord("Mary had");
		}
		
		[Test]
		public void ContainsWordMiddleCase()
		{
			var trueResult = "Mary had a little".ContainsWord("had");
			var falseResult = "Mary had a little lamb".ContainsWord("Aeroplane");
			
			Assert.That(trueResult, Is.True);
			Assert.That(falseResult, Is.False);
		}
		
		[Test]
		public void ContainsWordStartCase()
		{
			var trueResult = "Mary had a little".ContainsWord("Mary");
			
			Assert.That(trueResult, Is.True);
			
		}
		
		[Test]
		public void ContainsWordEndCase()
		{
			var trueResult = "Mary had a little".ContainsWord("little");
			
			Assert.That(trueResult, Is.True);
		}
		
		[Test]
		public void ContainsWordPunctuationCase()
		{
			var result1 = "Mary had a little lamb, it's feet...".ContainsWord("lamb");
			var result2 = "Mary had a little lamb.".ContainsWord("lamb");
			
			Assert.That(result1 && result2, Is.True);
		}
		
		
	}
}
