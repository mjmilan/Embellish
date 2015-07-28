
using System;
using System.Collections.Generic;
using System.Linq;

namespace Embellish.Dependencies
{
	/// <summary>
	/// This is a representation of an object that is being tracked.
	/// </summary>
	public class DependencyObject<T> where T:class
	{
		#region Properties
		protected WeakReference<DependencyDomain<T>> _domain = null;
		internal  List<WeakReference<DependencyObject<T>>> MyDependencies = new List<WeakReference<DependencyObject<T>>>();
		internal  List<WeakReference<DependencyObject<T>>> MyConsumers = new List<WeakReference<DependencyObject<T>>>();
		internal T UnderlyingObject {get; set;}
		public T ReferencedObject{
			get
			{
				return this.UnderlyingObject;
			}
		}
		public List<T> ItemsIDirectlyDependUpon{
			get
			{
				return MyDependencies.Select(x => {
				                             	DependencyObject<T> depObj;
				                             	if (x.TryGetTarget(out depObj))
			                             	    {
			                             	    	return depObj.UnderlyingObject;
			                             	    }
				                             	return null;
				                             }
				     ).ToList();
			}
		}
		#endregion
		
		#region Constructor
		internal DependencyObject(WeakReference<DependencyDomain<T>> domain, T underlyingObject)
		{
			if (underlyingObject == null) throw new ArgumentException("The underlyingObject has not been populated");
			_domain = domain;
			UnderlyingObject = underlyingObject;
		}
		#endregion
		#region Methods
		internal void AddDependency (T dependency){
			// Only act if we don't already have a record of the dependency
			if (!MyConsumers.Any(x => {
			                     	bool result = false;
			                     	DependencyObject<T> dep;
			                     	if (x.TryGetTarget(out dep))
			                     	{
			                     		result = dep.UnderlyingObject as T == dependency;
			                     	}
			                     	return result;
			                     }))
			{
				var newDependencyObject = new DependencyObject<T>(_domain, dependency);
				MyDependencies.Add(new WeakReference<DependencyObject<T>>(newDependencyObject));
			}
		}
		internal void RemoveDependencies (T dependencyToRemove)
		{
			var toRemove = MyDependencies.RemoveAll(x => 
			                                        {
			                                        	bool result = false;
			                                        	DependencyObject<T> d;
			                                        	if (x.TryGetTarget(out d))
			                                        	{
			                                        		result = d.UnderlyingObject == dependencyToRemove;
			                                        	}
			                                        	return result;
			                                        });
		}
		
		internal void RemoveReferencesFromOtherDependencies()
		{
			DependencyDomain<T> domain;
			if (_domain.TryGetTarget(out domain))
			{
				var otherDependencyObjects = domain.Items.Values.Where(x => x != this);
				foreach (var oDependency in otherDependencyObjects)
				{
					oDependency.RemoveDependencies(this.UnderlyingObject);
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
			foreach(var wr in target.MyDependencies)
			{
				DependencyObject<T> depObj;
				if (wr.TryGetTarget(out depObj))
				{
					if (!alreadyTested.Contains(depObj))
				    {
						var tuple = new Tuple<int, DependencyObject<T>>(iteration, depObj);
						recursionList.Add(tuple);
						alreadyTested.Add(depObj);
						RecursiveDependencyInfo(recursionList, iteration+1, depObj,alreadyTested);
				    }
				}
			}
			
		}
		
		internal List<T> AllDependencies()
		{
			var info = RecursiveObjectsIDependOnFullInfo().Select(x => x.Item2.UnderlyingObject).ToList();
			return info;
		}
		#endregion
		
	}
}
