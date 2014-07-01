
using System;
using NUnit.Framework;
using Embellish.RangeGuitar;

namespace Embellish.Tests
{
	[TestFixture]
	public class RangeGuitarTests
	{
		[Test]
		public void StringTests()
		{
			var guitar = new RangeGuitar<string>();
			guitar.ContainmentTest = ContainmentTestType.StartPointIncluded;
			GuitarString<string> gcse = guitar.createString();
			gcse.createItem("A","H","You've passed your GCSE");
			gcse.createItem("A","D", "You've done well...");
			gcse.createItem("C","D", "An average performance...");
			GuitarString<string> aLevel = guitar.createString();
			aLevel.createItem("A","F","You passed");
			aLevel.createItem("A","D","You did really well...");
			
			var gradeA = guitar.StringValuesAtPoint("A");
			var gradeC = guitar.StringValuesAtPoint("C");
			var gradeE = guitar.StringValuesAtPoint("E");
			var gradeG = guitar.StringValuesAtPoint("G");
			
			Assert.That(gradeA[gcse].ToString(), Is.EqualTo("You've done well..."));
			Assert.That(gradeE[gcse].ToString(), Is.EqualTo("You've passed your GCSE"));
			Assert.That(gradeC[gcse].ToString(), Is.EqualTo("An average performance..."));
			Assert.That(gradeA[aLevel].ToString(), Is.EqualTo("You did really well..."));
			Assert.That(gradeE[aLevel].ToString(), Is.EqualTo("You passed"));
			
		}
	}
}
