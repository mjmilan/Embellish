
using System;
using System.Collections.Generic;
using System.Linq;
namespace Embellish.RangeGuitar
{
	/// <summary>
	/// Representation of 
	/// </summary>
	public class GuitarString<T> where T : IComparable
	{
		#region Members
		internal WeakReference mGuitar = new WeakReference(null);
		internal SortedList<T, StringItem<T>> mlstItems = new SortedList<T, StringItem<T>>();
		#endregion
		
		#region Constructor
		/// <summary>
		/// Internal constructor to prevent the user trying to bypass the factory method on the guitar
		/// </summary>
		internal GuitarString()
		{
		}
		#endregion
		
		#region Properties
		public string StringName { get; set;}
        internal RangeGuitar<T> Guitar
        {
            get
            {
                if (mGuitar != null)
                {
                    return mGuitar.Target as RangeGuitar<T>;
                }
                return null;
            }
            set
            {
                mGuitar.Target = value;
            }
        }
		#endregion
		
		#region Methods
		public StringItem<T> createItem(T startPoint, T endPoint, object itemContent){
			StringItem<T> item = new StringItem<T>();
			item.StartPoint = startPoint;
			item.EndPoint = endPoint;
			item.String = this;
			item.Content = itemContent;
            this.addItem(item);

			return item;
		}
		
		public void addItem(StringItem<T> item){

			// Adds an item to the list, moving anything out of the way first.
			int i = 0;
			
			while (i < mlstItems.Count){
				StringItem<T> loopItem = mlstItems[mlstItems.Keys[i]];
				if (item != loopItem){
					IntersectionDetails intersectionInfo = StringItem<T>.examineIntersection(item, loopItem);
					switch (intersectionInfo){
						case IntersectionDetails.BInA:
							// B will be obliterated by A
							loopItem.String = null;
							mlstItems.RemoveAt(i);
							i--;
							break;
						
						case IntersectionDetails.AInB:
							// We need to split B (loopitem) into two seperate pieces.
							T origEndOfB = loopItem.EndPoint;
							loopItem.EndPoint = item.StartPoint;
							
							StringItem<T> newItem = this.createItem(item.EndPoint, origEndOfB, loopItem.Content);
							mlstItems.Add(newItem.StartPoint, newItem);
							break;
							
						case IntersectionDetails.IntersectionALowerThanB:
							// We need to move the start of B to the end of A.
							loopItem.StartPoint = item.EndPoint;
							if (loopItem.StartPoint.CompareTo(loopItem.EndPoint) == 0){
								loopItem.String = null;
								mlstItems.RemoveAt(i);
								i--;
							}
							break;
							
						case IntersectionDetails.IntersectionBLowerThanA:
							// We need to move the end of B to the start of A
							loopItem.EndPoint = item.StartPoint;
							if (loopItem.StartPoint.CompareTo(loopItem.EndPoint) == 0){
								loopItem.String = null;
								mlstItems.RemoveAt(i);
								i--;
							}
							break;
							
						case IntersectionDetails.NoIntersection:
							// There is no need for any interaction as these items do not intersect.
							break;
						
						default:
							break;
					}
					
				}
				i++;
			}
			mlstItems.Add(item.StartPoint, item);
					
			
		}
		
		public void clearString(){
			if (mlstItems != null){
				
				for (int i = 0; i < mlstItems.Count; i++){
					mlstItems[mlstItems.Keys[i]].String = null;
				}
				mlstItems.Clear();
			}
		}
		
		public StringItem<T> FindStringItemAtPoint(T point){
			StringItem<T> result = null;
			var item = (from c in mlstItems.Values 
				where c.IsPointWithinItem(point)
				select c).FirstOrDefault();
			result = item;
			return result;
		}
		
		public object FindObjectAtPoint(T point){
		
			object result = null;
			StringItem<T> item = FindStringItemAtPoint(point);
			if (item != null){
				result = item.Content;
			}
			return result;
		}
		
		
		
		#endregion
		
	}
}
