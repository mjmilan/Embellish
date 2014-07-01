
using System;

namespace Embellish.Observables
{
	/// <summary>
	/// Description of Observable.
	/// </summary>
	[System.ComponentModel.DefaultProperty("Item")]
	public class Observable<T> : IObservable<T>
	{
		#region Members
		private T _item;
		public event Action<object, Embellish.Observables.ChangeArguments<T>> ValueChanged;
		#endregion
		
		#region Constructor
		public Observable(T item)
		{
			// This is the one place we will allow the item to be set without triggering the event.
			_item = item;
		}
		#endregion
		

		
		#region Properties
		public T Item
		{
			get
			{
				return _item;
			}
			set
			{
				if (!(_item.Equals(value)))
				{
					T oldValue = _item;
					_item = value;
					
					if (ValueChanged != null){
						ChangeArguments<T> ca = new ChangeArguments<T>(oldValue, value);
						ValueChanged(this, ca);
					}
					
				}
			}
		}
		#endregion
		
	}
}
