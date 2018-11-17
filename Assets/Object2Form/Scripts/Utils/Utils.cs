using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace O2F
{
	public class Utils
	{
		public static string RemoveCloneMarkerFromString(string originalText)
		{
			if (string.IsNullOrEmpty(originalText))
			{
				return null;
			}

			return originalText.Replace("(Clone)", string.Empty);
		}

		public static string ClearGenericNaming(string fieldName)
		{
			fieldName = fieldName.Replace("<", string.Empty);
			fieldName = fieldName.Replace(">", string.Empty);

			return fieldName;
		}

		public static string CapitalizeFirstLetter(string originalText)
		{
			if (string.IsNullOrEmpty(originalText))
			{
				return null;
			}

			return originalText.First().ToString().ToUpper() + originalText.Substring(1);
		}

		public static Type GetElementTypeOfEnumerable(Type type)
		{
			Type[] interfaces = type.GetInterfaces();

			return interfaces
				.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				.Select(x => x.GetGenericArguments()[0]).FirstOrDefault();
		}

		public static T GetDefaultGeneric<T>()
		{
			return default(T);
		}

		public static T ExtractAttributeFromFieldInfo<T>(FieldInfo field) where T : Attribute
		{
			if (field != null)
			{
				foreach (object attr in field.GetCustomAttributes(typeof(T), false))
				{
					if (attr is T)
					{
						return attr as T;
					}
				}
			}

			return null;
		}
	}
}