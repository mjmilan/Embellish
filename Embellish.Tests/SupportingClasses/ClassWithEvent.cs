
using System;

namespace Embellish.Tests.SupportingClasses
{

	public class ClassWithEvent
	{
		public ClassWithEvent()
		{
		}
		
		public event EventHandler SomethingHappened;
		
		public void MakeSomethingHappen()
		{
			if (this.SomethingHappened != null)
			{
				SomethingHappened(this, new EventArgs());
			}
		}
	}
}
