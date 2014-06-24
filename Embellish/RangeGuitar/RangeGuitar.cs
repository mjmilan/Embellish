
using System;
using System.Collections.Generic;
using System.Linq;
namespace Embellish.RangeGuitar
{
	/// <summary>
	/// Description of RangeGuitar.
	/// </summary>
	public class RangeGuitar <T> where T : IComparable
	{
		#region Members
		internal Func<T, StringItem<T>, bool> mContainsDelegate;
		internal ContainmentTestType mContainmentTestType;
		internal List<GuitarString<T>> mlstStrings = new List<GuitarString<T>>();
		#endregion
		
		#region Constructor
		public RangeGuitar()
		{
			this.ContainmentTest = ContainmentTestType.StartPointIncluded;
		}
		#endregion
		
		#region Properties
		public ContainmentTestType ContainmentTest{
			get{return mContainmentTestType;}
			set{
				mContainmentTestType = value;
				switch (mContainmentTestType){
					case ContainmentTestType.StartPointIncluded :
						mContainsDelegate = new Func<T, StringItem<T>, bool>(containmentTestWithStartIncluded);
						break;
					case ContainmentTestType.EndPointIncluded :
						mContainsDelegate = new Func<T, StringItem<T>, bool>(containmentTestWithEndIncluded);
						break;
					default:
						break;
				}
			}
				
		}
		
		public IEnumerable<GuitarString<T>> GuitarStrings{
			get{ return mlstStrings;}
		}
	
		#endregion
		
		#region Methods
		public GuitarString<T> createString(){
			GuitarString<T> newString = new GuitarString<T>();
			newString.Guitar = this;
            this.addString(newString);
			return newString;
		}
		
		public void addString(GuitarString<T> gs){
			mlstStrings.Add(gs);
		}
		public void removeString(GuitarString<T> gs){
			mlstStrings.Remove(gs);
		}
		
		internal bool containmentTestWithStartIncluded(T point, StringItem<T> guitarString){
			bool result = false;
			if (point.CompareTo(guitarString.StartPoint) > -1) result = true; //
			if (point.CompareTo(guitarString.EndPoint) > -1) result = false;
			return result;
		}
		
		internal bool containmentTestWithEndIncluded(T point, StringItem<T> guitarString){
			bool result = false;
			if (point.CompareTo(guitarString.StartPoint) == 1) result = true; //
			if (point.CompareTo(guitarString.EndPoint) == 1) result = false;
			return result;
		}
		
		public Dictionary<GuitarString<T>, object> StringValuesAtPoint(T point){
			Dictionary<GuitarString<T>, object> results = new Dictionary<GuitarString<T>, object>();
			foreach(GuitarString<T> gs in this.mlstStrings){
				if (gs != null){
					results.Add(gs, gs.FindObjectAtPoint(point));
				}
			}
			return results;
		}
	
		public List<RangeBand<T>> GetRangeBands(){
			List<RangeBand<T>> results = new List<RangeBand<T>>();
			List<T> lstRangePoints = new List<T>();
			Action<T> addIfNotInList = x => {
				if (!lstRangePoints.Contains(x)) lstRangePoints.Add(x);
			};
			
			foreach (GuitarString<T> gs in this.GuitarStrings){
				foreach(StringItem<T> si in gs.mlstItems.Values){
					addIfNotInList(si.StartPoint);
					addIfNotInList(si.EndPoint);
				}
			}
			
            // Now sort the list into T order...
            lstRangePoints = lstRangePoints.OrderBy(x => x).ToList();

			// Ok - we now have a list of distinct range points to play with.
			if (lstRangePoints.Count > 1){
				for (int cnt = 0; cnt < lstRangePoints.Count - 1; cnt++){
					RangeBand<T> rb = new RangeBand<T>();
					rb.StartPoint = lstRangePoints[cnt];
					rb.EndPoint = lstRangePoints[cnt + 1];
					T testPoint;
					if (this.ContainmentTest == ContainmentTestType.StartPointIncluded){
						testPoint = rb.StartPoint;
					}
					else{
						testPoint = rb.EndPoint;
					}
					rb.StringValues = this.StringValuesAtPoint(testPoint);
					results.Add(rb);
				}
			}
			
			return results;
			
		}
		
		#endregion
	}
	
	public enum ContainmentTestType{
		StartPointIncluded,
		EndPointIncluded
	}
}
