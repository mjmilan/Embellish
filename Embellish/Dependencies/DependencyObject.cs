
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace Embellish.Dependencies
{
	/// <summary>
	/// This is a representation of an object that is being tracked.
	/// </summary>
	internal class DependencyObject<T> where T:class
	{
		#region Properties
		protected WeakReference<DependencyDomain<T>> _domain = null;
		internal  ObservableCollection<DependencyObject<T>> MyDependencies = new ObservableCollection<DependencyObject<T>>();
		internal  List<DependencyObject<T>> MyConsumers = new List<DependencyObject<T>>();
		internal T UnderlyingObject {get; set;}

		public List<T> ItemsIDirectlyDependUpon{
			get
			{
				return MyDependencies.Select(x => x.UnderlyingObject).ToList();
			}
		}
		#endregion
		
		#region Constructor
		internal DependencyObject(WeakReference<DependencyDomain<T>> domain, T underlyingObject)
		{
			if (underlyingObject == null) throw new ArgumentException("The underlyingObject has not been populated");
			_domain = domain;
			UnderlyingObject = underlyingObject;
			MyDependencies.CollectionChanged += this.DepencyChangeHandler;
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Record that the supplied object depends upon this one.
		/// </summary>
		/// <param name="dependency">Supplied object</param>
		internal void AddDependency (T dependency){
			// Only act if we don't already have a record of the dependency
			if (!MyDependencies.Any(x => x.UnderlyingObject == dependency))
			{
				DependencyDomain<T> domain;
				DependencyObject<T> newDependencyObject;
				if (_domain.TryGetTarget(out domain))
				{
					newDependencyObject = domain.Items[dependency];
				}
				else
				{
					newDependencyObject = new DependencyObject<T>(_domain, dependency);
				}
				MyDependencies.Add(newDependencyObject);
			}
		}
		
		/// <summary>
		/// Remove this object from the dependencies list on all other objects in the dependency domain.
		/// </summary>
		internal void RemoveReferencesFromOtherDependencies()
		{
			DependencyDomain<T> domain;
			if (_domain.TryGetTarget(out domain))
			{
				var otherDependencyObjects = domain.Items.Values.Where(x => x != this);
				foreach (var oDependency in otherDependencyObjects)
				{
					oDependency.RemoveDependency(this.UnderlyingObject);
				}
			}
		}
		
		
		
		internal List<Tuple<int, DependencyObject<T>>> RecursiveObjectsIDependOnFullInfo()
		{
			var recursiveList = new List<Tuple<int, DependencyObject<T>>>();
			var alreadyTested = new List<DependencyObject<T>>();
			RecursiveDependencyInfo(recursiveList, 0, this, alreadyTested);
			return recursiveList.OrderBy(x => x.Item1).ToList();
		}
		
		protected void RecursiveDependencyInfo (List<Tuple<int, DependencyObject<T>>> recursionList, int iteration, DependencyObject<T> target, List<DependencyObject<T>> alreadyTested)
		{
			DependencyDomain<T> ourDomain;
			_domain.TryGetTarget(out ourDomain);
			
			foreach(var d in target.MyDependencies)
			{
				var depObj = ourDomain._items[d.UnderlyingObject];
				if (!alreadyTested.Contains(depObj))
			    {
					var tuple = new Tuple<int, DependencyObject<T>>(iteration, depObj);
					recursionList.Add(tuple);
					alreadyTested.Add(depObj);
					RecursiveDependencyInfo(recursionList, iteration+1, depObj,alreadyTested);
			    }
			}
			
		}
		
		/// <summary>
		/// Gets a list of all objects that this object depends upon...
		/// </summary>
		/// <returns>Returns a list of all objects that this object depends upon</returns>
		internal List<T> AllDependencies()
		{
			var info = RecursiveObjectsIDependOnFullInfo().Select(x => x.Item2.UnderlyingObject).ToList();
			return info;
		}
		
		/// <summary>
		/// Remove the specified object from this object's list of dependencies.
		/// </summary>
		/// <param name="dependency"></param>
		internal void RemoveDependency(T dependency)
		{
			var toBeRemoved = this.MyDependencies.Where(x => x.UnderlyingObject == dependency).ToList();
			foreach (var d in toBeRemoved)
			{
				MyDependencies.Remove(d);
			}
			
		}
		
		/// <summary>
		/// Determines whether this object has any dependency (direct or indirect) on the supplied test object
		/// </summary>
		/// <param name="testObject"></param>
		/// <returns>True of there is a dependency, otherwise false...=</returns>
		internal bool DependsUpon(T testObject){
			return RecursiveObjectsIDependOnFullInfo().Any(x => x.Item2.UnderlyingObject == testObject);
		}
		#endregion
		
		#region Event Handlers
		/// <summary>
		/// Event handler for detecting when where is a change to the dependent objects...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="changeArguments"></param>
		protected void DepencyChangeHandler(Object sender,	NotifyCollectionChangedEventArgs changeArguments)
		{
			if (changeArguments.Action == NotifyCollectionChangedAction.Add)
			{
				// We are adding dependencies
				foreach (var d in changeArguments.NewItems)
				{
					DependencyObject<T> dep = (DependencyObject<T>)d;
					dep.MyConsumers.Add(this);
				}
			}
			else if(changeArguments.Action == NotifyCollectionChangedAction.Remove)
			{
				// We are removing dependencies
				foreach (var d in changeArguments.OldItems)
				{
					DependencyObject<T> dep = (DependencyObject<T>)d;
					if (dep.MyConsumers.Contains(this))
					{
						dep.MyConsumers.Remove(this);
					}
				}
			}
		}
		#endregion
	}
}
