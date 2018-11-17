using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace O2F
{
	public class TypeUIComponentBinder
	{
		private static Dictionary<Type, Type> binderDictionary = null;

		public static void InitializeBindings()
		{
			binderDictionary = new Dictionary<Type, Type>()
			{
				{ typeof(String), typeof(UITextField) },
				{ typeof(Int16), typeof(UITextField) },
				{ typeof(Int32), typeof(UITextField) },
				{ typeof(Int64), typeof(UITextField) },
				{ typeof(Single), typeof(UITextField) },
				{ typeof(Double), typeof(UITextField) },
				{ typeof(Char), typeof(UITextField) },
				{ typeof(Byte), typeof(UITextField) },
				{ typeof(Boolean), typeof(UICheckBox) }
			};
		}

		public static Type GetTypeBySystemType(Type type)
		{
			if(binderDictionary == null)
			{
				InitializeBindings();
			}

			Type result = null;
			binderDictionary.TryGetValue(type, out result);

			return result;
		}
	}
}