
using System;
using System.Collections.Generic;
using System.Linq;
namespace Embellish.Dependencies
{
	/// <summary>
	/// This class serves as a domain for dependency operations..
	/// </summary>
	public class DependencyDomain<T> where T:class
	{
		#region Members
		internal Dictionary<T, DependencyObject<T>> _items = new Dictionary<T, DependencyObject<T>>();
		#endregion
		
		#region Properties
		internal  Dictionary<T, DependencyObject<T>> Items{
			get
			{
				return _items;
			}
		}
		#endregion
		
		#region Constructor
		public DependencyDomain()
		{
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Adds an object onto the dependency domain.
		/// </summary>
		/// <param name="objectToAdd">Object to add</param>
		public void AddToDomain(T objectToAdd)
		{
			if (!_items.ContainsKey(objectToAdd))
			{
				_items[objectToAdd] = new DependencyObject<T>(new WeakReference<DependencyDomain<T>>(this), objectToAdd);
			}
		}
		
		/// <summary>
		/// Removes an object from the dependency domain.
		/// </summary>
		/// <param name="objectToRemove">The object to remove.</param>
		public void RemoveReferencesFromDomain(T objectToRemove)
		{
			if (_items.ContainsKey(objectToRemove))
			{
				_items[objectToRemove].RemoveReferencesFromOtherDependencies();
				_items.Remove(objectToRemove);
			}
		}
		
		/// <summary>
		/// Removes all objects from the dependency domain.
		/// </summary>
		public void ClearDomain()
		{
			foreach(var obj in _items.Keys)
			{
				_items[obj].RemoveReferencesFromOtherDependencies();
			}
			_items.Clear();
		
		}
		
		/// <summary>
		/// Gets a list of objects that are directly depended upon by a specified target object 
		/// </summary>
		/// <param name="target">A target object</param>
		/// <returns>A list of objects that the target object directly depends upon</returns>
		public List<T> GetDirectDependenciesForObject(T target)
		{
			if (_items.ContainsKey(target))
			{
				return _items[target].ItemsIDirectlyDependUpon;
			}
			else
			{
				throw new ArgumentException ("The target is not within this dependency domain...");
			}
		}
		
		/// <summary>
		/// Gets a list of all objects that a specified target object depends upon, both directly and indirectly...
		/// </summary>
		/// <param name="target">The specified target object</param>
		/// <returns>A list of objects that the specified object depends upon.</returns>
		public List<T> GetAllDependenciesForObject(T target)
		{
			if (_items.ContainsKey(target))
			{
				return _items[target].AllDependencies();
			}
			else
			{
				throw new ArgumentException ("The target is not within this dependency domain...");
			}
		}
		
		/// <summary>
		/// Gets a list of all the items currently registered on the dependency domain.
		/// </summary>
		/// <returns>A list of all the items currently registered on the dependency domain.</returns>
		public List<T> DomainItems()
		{
			var items = _items.Values.GetEnumerator();
			var result = new List<T>();
			
			while (items.MoveNext())
			{
				result.Add(items.Current.UnderlyingObject);
			}
			
			return result;
		}
		
		/// <summary>
		/// Registers a direct dependency between a specified target object and an object it depends upon...
		/// </summary>
		/// <param name="target">Specified target object</param>
		/// <param name="dependency">Object that target directly depends upon.</param>
		public void AddDependency(T target, T dependency)
		{
			if (!_items.ContainsKey(target))
			{
				this.AddToDomain(target);
			}
			
			if (!_items.ContainsKey(dependency))
			{
				this.AddToDomain(dependency);
			}
			
			_items[target].AddDependency(dependency);
			
		}
		
		/// <summary>
		/// Removes a dependency between the specifed object and an object it depends upon.
		/// </summary>
		/// <param name="target">Target object</param>
		/// <param name="dependency">Depended upon object</param>
		public void RemoveDependency(T target, T dependency)
		{
			if (_items.ContainsKey(target))
			{
				_items[target].RemoveDependency(dependency);
			}
		}
		
		/// <summary>
		/// Determines whether object a depends upon object b
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool DoesADependOnB(T a, T b)
		{
			if(!(_items.ContainsKey(a) && _items.ContainsKey(b)))
			{
				throw new ArgumentException ("You have supplied an object that is not within this DependencyDomain");
			}
			
			return _items[a].DependsUpon(b);
		}
		
		/// <summary>
		/// Determines whether b depends upon a...
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public bool IsAConsumedByB(T a, T b)
		{
			// Turning the question about ot it's head, we can reverse this and simply ask if B depends on A...
			return DoesADependOnB(b,a);
		}
		
		/// <summary>
		/// Returns a list of objects that are direct consumers of the specified target object.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public List<T> DirectConsumersOfTarget(T target)
		{
			if (_items.ContainsKey(target))
			{
				var depObj = _items[target];
				return depObj.MyConsumers.Select(x => x.UnderlyingObject).ToList();
			}
			else
			{
				throw new ArgumentException("The object you have passed in is not a member of this dependecy domain...");
			}
		}
		
		/// <summary>
		/// Returns a list of all objects in the dependency domain that are not dependent upon other objects.
		/// </summary>
		/// <returns>A list of all objects in the dependency domain that are not dependent upon other objects.</returns>
		public List<T> RootLevelObjects()
		{
			List<T> results;
			results = _items.Values.Where(x => x.MyDependencies.Count == 0).Select(x => x.UnderlyingObject).ToList();
			return results;
		}
		
		#endregion
	}
}
