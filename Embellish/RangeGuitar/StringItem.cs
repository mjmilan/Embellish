
using System;
using System.Collections.Generic;

namespace Embellish.RangeGuitar
{
	/// <summary>
	/// Represents an item on a Guitar String, where the guitar is ranged by datatype T.
	/// </summary>
	public class StringItem<T> : RangeGuitar.Range<T> where T : IComparable 
	{
		#region Members
		protected object mitemContent;
		internal WeakReference mString = new WeakReference(null);
		#endregion
		
		#region Constructor
		internal StringItem(){
			// Default constructor, specified here to prevent clients avoiding the factory method in GuitarString
			// by calling the default construtor publicly.
		}
		#endregion
		
		#region Properties

		
		public object Content{
			get{return mitemContent;}
			set{mitemContent = value;}
		}
		
		public GuitarString<T> String{
			internal get{
                if (mString != null)
                {
                    if (mString.Target is GuitarString<T>)
                    {
                        return mString.Target as GuitarString<T>;
                    }
                }
                return null;
			}
			set{
                mString.Target = value;
			}
		}
		
		#endregion
		
		#region Method
		public override void Reset(){
			base.Reset();
			this.Content = null;
		}
		
		public bool IsPointWithinItem(T point){
			bool result = false;
			if (this.String != null)
			{
				RangeGuitar.RangeGuitar<T> g = this.String.Guitar;
				if (g != null){
					result = g.mContainsDelegate.Invoke(point, this);
				}
			}
			return result;
		}
		#endregion
		
		#region Static Methods
		internal static IntersectionDetails examineIntersection(StringItem<T> A, StringItem<T> B){
		
			if (A.StartPoint.CompareTo(B.StartPoint) != 1 && A.EndPoint.CompareTo(B.EndPoint) != -1)
				return IntersectionDetails.BInA;
			if (B.StartPoint.CompareTo(A.StartPoint) != 1 && B.EndPoint.CompareTo(A.EndPoint) != -1)
				return IntersectionDetails.AInB;
			if (A.EndPoint.CompareTo(B.StartPoint) == -1 || B.EndPoint.CompareTo(A.StartPoint) == -1)
				return IntersectionDetails.NoIntersection;
			int AWithB = A.StartPoint.CompareTo(B.StartPoint);
			if (AWithB != 0){
				// This can be determined on start points.
				if (AWithB == 1){
					return IntersectionDetails.IntersectionBLowerThanA;
				}
				else{
					return IntersectionDetails.IntersectionALowerThanB;
				}
			}
			else{
				// We'll have to use end points to settle this.
				if (A.EndPoint.CompareTo(B.EndPoint) != 1 ){
					return IntersectionDetails.IntersectionALowerThanB;
				}
				else{
					return IntersectionDetails.IntersectionBLowerThanA;
				}
			}
		}
		#endregion
	}
	
	public enum IntersectionDetails{
		NoIntersection,
		AInB,
		BInA,
		IntersectionALowerThanB,
		IntersectionBLowerThanA
	}
}