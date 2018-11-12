using System;

namespace O2F
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class TextFieldAttribute : Attribute
	{
		public ETextFieldType textFieldType;
		public bool readOnly;

		public TextFieldAttribute()
		{
			textFieldType = ETextFieldType.String;
		}

		public TextFieldAttribute(ETextFieldType textFieldType)
		{
			this.textFieldType = textFieldType;
		}

		public TextFieldAttribute(bool readOnly) : this()
		{
			this.readOnly = readOnly;
		}

		public TextFieldAttribute(ETextFieldType textFieldType, bool readOnly)
		{
			this.readOnly = readOnly;
		}
	}
}