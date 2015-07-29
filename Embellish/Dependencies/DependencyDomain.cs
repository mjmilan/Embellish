
using System;
using System.Collections.Generic;
namespace Embellish.Dependencies
{
	/// <summary>
	/// This class serves as a domain for dependency operations..
	/// </summary>
	public class DependencyDomain<T> where T:class
	{
		#region Members
		public Dictionary<T, DependencyObject<T>> _items = new Dictionary<T, DependencyObject<T>>();
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
		public void AddToDomain(T objectToAdd)
		{
			if (!_items.ContainsKey(objectToAdd))
			{
				_items[objectToAdd] = new DependencyObject<T>(new WeakReference<DependencyDomain<T>>(this), objectToAdd);
			}
		}
		public void RemoveReferencesFromDomain(T objectToRemove)
		{
			if (_items.ContainsKey(objectToRemove))
			{
				_items[objectToRemove].RemoveReferencesFromOtherDependencies();
				_items.Remove(objectToRemove);
			}
		}
		
		public void ClearDomain()
		{
			foreach(var obj in _items.Keys)
			{
				_items[obj].RemoveReferencesFromOtherDependencies();
			}
			_items.Clear();
		
		}
		
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
		
		public void RemoveDependency(T target, T dependency)
		{
			if (_items.ContainsKey(target))
			{
				_items[target].RemoveDependency(dependency);
			}
		}
		
		public bool DoesADependOnB(T a, T b)
		{
			if(!(_items.ContainsKey(a) && _items.ContainsKey(b)))
			{
				throw new ArgumentException ("You have supplied an object that is not within this DependencyDomain");
			}
			
			return _items[a].DependsUpon(b);
			
		}
		#endregion
	}
}
