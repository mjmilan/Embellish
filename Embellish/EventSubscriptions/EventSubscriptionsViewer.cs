
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Embellish.EventSubscriptions
{
	/// <summary>
	/// Description of EventSubscriptionsManager.
	/// </summary>
	public class EventSubscriptionsViewer
	{
		#region Members
		protected object _eventSource;
		#endregion
		
		#region Constructor
		public EventSubscriptionsViewer(object eventSource)
		{
			_eventSource = eventSource;
		}
		#endregion
		
		public ReadOnlyCollection<EventInformation> Events 
		{
			get
			{
				return new ReadOnlyCollection<EventInformation> (BuildEventInformationForAllEvents().ToList());
			}
		}
		
		protected IEnumerable<EventInformation> BuildEventInformationForAllEvents()
		{
			var underlyingType = _eventSource.GetType();
			var eventInfo = underlyingType.GetEvents();
			foreach(var ei in eventInfo)
			{
				EventHandler eh = null;
				var fieldInfo = underlyingType.GetField(ei.Name, BindingFlags.Instance | BindingFlags.NonPublic);
				eh = (EventHandler)(fieldInfo.GetValue(_eventSource));
				yield return new EventInformation(ei, eh, _eventSource);
			}
		}
		
		public EventInformation GetEventInformationForNamedEvent(string eventName)
		{
			var ei = _eventSource.GetType().GetEvent(eventName);
			if (ei == null) throw new ArgumentException(String.Format("The event \"{0}\" is not declared on the object", eventName));
			var fieldInfo = _eventSource.GetType().GetField(ei.Name, BindingFlags.Instance | BindingFlags.NonPublic);
			EventHandler eh = (EventHandler)(fieldInfo.GetValue(_eventSource));
			return new EventInformation(ei, eh, _eventSource);

		}
		
	}
}
