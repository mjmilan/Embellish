
using System;
using System.Collections.Generic;

namespace Embellish.RangeGuitar
{
	/// <summary>
	/// Description of RangeBand.
	/// </summary>
	public class RangeBand<T> : Range<T> where T:IComparable
	{
		#region Members
		internal Dictionary<GuitarString<T>, object> mDictionary = new Dictionary<GuitarString<T>, object>();
		#endregion
		
		#region Properties
		public Dictionary<GuitarString<T>, object> StringValues{
			get{return mDictionary;}
			internal set{mDictionary = value;}
		}
		
		#endregion
		
		
	}
}
