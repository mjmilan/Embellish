
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Embellish.Observables;


namespace Embellish.Tests
{

	[TestFixture]
	public class ObservableTests
	{
		private bool _eventFired = false;
		
		[Test]
		[TestCase(1,2)]
		[TestCase("A","B")]
		public void CheckEventFiresWhenItShould<T> (T firstValue, T secondValue)
		{
			// Arrange
			_eventFired = false;
			T first = default(T);
			T second = default(T);
			Observable<T> obs = new Observable<T>(firstValue);
			obs.ValueChanged += (o, ca) => 
			{
				first = ca.OldValue;
				second = ca.NewValue;
				_eventFired = true;
			};
		
			// Act
			obs.Item = secondValue;
			
			// Assert
			Assert.That(_eventFired, Is.True);
			Assert.That(first, Is.EqualTo(firstValue));
			Assert.That(second, Is.EqualTo(secondValue));
			
		}
		
		[Test]
		[TestCase(2,2)]
		[TestCase("A","A")]
		public void CheckEventDoesNotFireWhenItShouldNot<T>(T firstValue, T secondValue)
		{
			// Arrange
			_eventFired = false;
			Observable<T> obs = new Observable<T>(firstValue);
			obs.ValueChanged += (o, ca) => _eventFired = true;
			
			// Act
			obs.Item = secondValue;
			
			// Assert
			Assert.That(_eventFired, Is.False);
		}
		
	}
}