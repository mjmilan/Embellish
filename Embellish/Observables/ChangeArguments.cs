
using System;

namespace Embellish.Observables
{

	public class ChangeArguments<T>
	{
		public ChangeArguments(T oldValue, T newValue)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
		
		public T OldValue {get; protected set;}
		public T NewValue {get; protected set;}
		
	}
}