using System;

namespace O2F
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ObjectFieldAttribute : Attribute
	{
		public bool removable;

		public ObjectFieldAttribute(bool removable = false)
		{
			this.removable = removable;
		}
	}
}