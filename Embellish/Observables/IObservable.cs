
using System;

namespace Embellish.Observables
{
	
	
	public interface IObservable<T>
	{
		T Item{get;}
		event Action<object, ChangeArguments<T>> ValueChanged;
	}
}