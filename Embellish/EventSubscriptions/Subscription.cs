
using System;

namespace Embellish.EventSubscriptions
{
	/// <summary>
	/// Description of Subscription.
	/// </summary>
	public class Subscription
	{
		#region Members
		protected Delegate _delegate;
		#endregion
		
		#region Properties
		public System.Delegate Delegate
		{
			get
			{
				return _delegate;
			}
		}
		
		public object TargetObject
		{
			get
			{	
				return _delegate.Target;
			}
				
		}
		
		public string MethodName
		{
			get
			{
				return _delegate.Method.Name;
			}
		}
	
		#endregion
		
		
		#region Constructor
		public Subscription(Delegate d)
		{
			_delegate = d;
		}
		#endregion
		
	}
}
