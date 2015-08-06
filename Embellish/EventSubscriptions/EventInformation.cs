
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Embellish.EventSubscriptions
{
	/// <summary>
	/// Description of EventInformation.
	/// </summary>
	public class EventInformation
	{
		#region Members
		protected System.Reflection.EventInfo _ei;
		protected object _eventSource;
		protected EventHandler _eventHandler;
		#endregion
		
		#region Constructor
		public EventInformation(EventInfo ei, EventHandler eh, object eventSource)
		{
			_ei = ei;
			_eventSource = eventSource;
			_eventHandler = eh;
			
		}
		#endregion
		
		#region Properties
		public string EventName {
			get
			{
				return _ei.Name;
			}
		}
		
		public ReadOnlyCollection<Subscription> SubscriptionList 
		{
			get
			{
				List<Subscription> result = new List<Subscription>();
				Delegate[] delegates;
				if (_ei.IsMulticast)
				{
					delegates = _eventHandler.GetInvocationList();
				}
				else
				{
					delegates = new Delegate[] {_eventHandler};
				}
				
				foreach (var d in delegates)
				{
					var s = new Subscription(d);
					result.Add(s);
				}
				
				return new ReadOnlyCollection<Subscription>(result);
			}
		}
		
		#endregion
	}
}
