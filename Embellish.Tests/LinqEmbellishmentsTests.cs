
using System;
using System.Linq;
using NUnit.Framework;
using Embellish.LinqExtensions;
namespace Embellish.Tests
{
	/// <summary>
	/// Description of LinqEmbellishmentsTests.
	/// </summary>
    [TestFixture]
	public class LinqEmbellishmentsTests
	{
        private int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        [Test]
        public void CheckFirstNMatchingCondition()
        {
            var expected = new int[] { 2, 4, 6 };
            var firstThreeEvens = data.FirstNMatchingCondition(3, x => x % 2 == 0);

            bool match = expected.SequenceEqual(firstThreeEvens);

            Assert.That(match, Is.True);
        }

        [Test]
        public void CheckLastNMatchingCondition()
        {
            var expected = new int[] { 16, 18, 20 };
            var lastThreeEvens = data.LastNMatchingCondition(3, x => x % 2 == 0);
            bool match = expected.SequenceEqual(lastThreeEvens);

            Assert.That(match, Is.True);
        }

        [Test]
        public void CheckFirstN()
        {
            var expected = new int[] { 1, 2, 3, 4, 5 };
            var firstFive = data.FirstN(5);
            bool match = expected.SequenceEqual(firstFive);

            Assert.That(match, Is.True);
        }

        [Test]
        public void CheckLastN()
        {
            var expected = new int[] { 16,17,18,19,20 };
            var lastFive = data.LastN(5);
            bool match = expected.SequenceEqual(lastFive);

            Assert.That(match, Is.True);
        }


	}
}
