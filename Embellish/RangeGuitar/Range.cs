
using System;

namespace Embellish.RangeGuitar
{
	/// <summary>
	/// Description of Range.
	/// </summary>
	public class Range<T> : object where T:IComparable
	{
		#region Members
		protected T mstartPoint;
		protected T mendPoint;
		protected bool startPointSpecified = false;
		protected bool endPointSpecified = false;
		#endregion
		
		#region Properties
		public T StartPoint{
			get{
				return mstartPoint;
			}
			internal set{ 
				bool valid = true;
				if (endPointSpecified && (value.CompareTo(mendPoint) == 1))
					valid = false;
				if (!valid) throw new ArgumentException("The specified start point is greater than the endpoint of this item.");
				mstartPoint = value;
			}
		}
		
		public T EndPoint{
			get { return mendPoint;}
			internal set{ 
				bool valid = true;
				if (startPointSpecified && (value.CompareTo(mstartPoint) == -1)) 
					valid = false;
				if (!valid) throw new ArgumentException("The specified end point is less than the specified start point of this item.");
				mendPoint = value;
			}
		}
		#endregion
		
		#region Methods
		public virtual void Reset(){
			this.startPointSpecified = false;
			this.endPointSpecified = false;
			this.mstartPoint = default(T);
			this.mendPoint = default(T);
		}
		#endregion
		
		public Range()
		{
		}
	}
}
