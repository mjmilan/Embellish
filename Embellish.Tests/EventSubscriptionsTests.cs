
using System;
using NUnit.Framework;

namespace Embellish.Tests
{
	/// <summary>
	/// Description of EventSubscriptionsTests.
	/// </summary>
	[TestFixture]
	public class EventSubscriptionsTests
	{
		[Test]
		public void DetectsEventsAndSubscribers()
		{
			// Arrange
			var objWithEvents = new SupportingClasses.ClassWithEvent();
			objWithEvents.SomethingHappened += EventHandler1;
			objWithEvents.SomethingHappened += EventHandler2;

			// Act
			var manager = new Embellish.EventSubscriptions.EventSubscriptionsManager(objWithEvents);
			
			// Assert
			Assert.That(manager.Events.Count, Is.EqualTo(1));
			Assert.That(manager.Events[0].SubscriptionList.Count, Is.EqualTo(2));	
		}
		
		[Test]
		public void GetIndividualEvent()
		{
			// Arrange
			var objWithEvents = new SupportingClasses.ClassWithEvent();
			objWithEvents.SomethingHappened += EventHandler1;
			objWithEvents.SomethingHappened += EventHandler2;

			// Act
			var manager = new Embellish.EventSubscriptions.EventSubscriptionsManager(objWithEvents);
			var eventInfo = manager.GetEventInformationForNamedEvent("SomethingHappened");
			var subscribers = eventInfo.SubscriptionList;
			
			// Assert
			Assert.That(eventInfo, Is.Not.Null);
			Assert.That(subscribers.Count, Is.EqualTo(2));
		}
		
		private void EventHandler1(object sender, EventArgs e)
		{
			
		}
		
		private void EventHandler2(object sender, EventArgs e)
		{
			
		}
		
		
	}
}
