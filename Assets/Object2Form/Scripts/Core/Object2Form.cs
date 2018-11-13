using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace O2F
{
	public class Object2Form : MonoBehaviour
	{
		public static Object2Form Instance { get; private set; }

		[Header("Prefabs")]
		public UIForm uiFormPrefab;
		public UITextField uiTextFieldPrefab;
		public UICheckBox uiCheckBoxPrefab;
		public UIDropDown uiDropDownPrefab;
		public UIListElement uiListElementPrefab;

		[Header("Instances")]
		public UIForm uiFormInstance;

		private void Awake()
		{			
			Instance = this;
		}

		public void CreateEditForm(object templateObject)
		{
			if (templateObject != null)
			{
				//uiForm
				uiFormInstance = CreateUIForm(templateObject.GetType().Name);

				if (uiFormInstance != null)
				{
					uiFormInstance.SetReferenceObject(templateObject);
					ConvertObjectToFormUIs(templateObject, uiFormInstance.FormBody, true);
				}
			}
		}

		public void CreateNewForm<T>() where T : class, new()
		{
			T instance = new T();
			CreateEditForm(instance);
		}

		public void ConvertObjectToFormUIs(object templateObject, RectTransform parent, bool addToForm)
		{
			FieldInfo[] fields = templateObject.GetType().GetFields(
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			//All the fields in the templateObject
			foreach (FieldInfo field in fields)
			{
				string fieldName = field.Name;

				fieldName = fieldName.Replace("<", string.Empty);
				fieldName = fieldName.Replace(">", string.Empty);

				fieldName = CapitalizeFirstLetter(fieldName);

				ProcessTextFieldAttribute(field, fieldName, templateObject, parent, addToForm);
				ProcessCheckBoxAttribute(field, fieldName, templateObject, parent, addToForm);
				ProcessDropDown(field, fieldName, parent, addToForm);
				ProcessListElement(field, fieldName, templateObject, parent, addToForm);
			}
		}

		private UIForm CreateUIForm(string title)
		{
			UIForm uiForm = null;

			if(uiFormPrefab != null)
			{
				uiForm = Instantiate(uiFormPrefab, transform);

				if (!string.IsNullOrEmpty(title) && uiForm != null)
				{
					uiForm.name = RemoveCloneMarkerFromString(uiForm.name) + title;
					uiForm.SetTitleText(title);
				}
			}

			return uiForm;
		}

		private T CreateUIComponent<T>(Component prefab, string label, string value, Transform parent) where T : UIComponent
		{
			T uiComponent = null;

			if (prefab != null)
			{
				uiComponent = Instantiate(prefab, parent).GetComponent<T>();

				if (uiComponent != null)
				{
					string objectName = RemoveCloneMarkerFromString(uiComponent.name);

					uiComponent.name = objectName + label;
					uiComponent.SetLabel(label);
					uiComponent.SetValue(value);
					uiComponent.SetReadOnly(false);
				}
			}

			return uiComponent;
		}

		private UIListElement CreateUIListElement(string title, Transform parent)
		{
			UIListElement uiListElement = null;

			if(uiListElementPrefab != null)
			{
				uiListElement = Instantiate(uiListElementPrefab, parent);

				if(uiListElement != null)
				{
					string objectName = RemoveCloneMarkerFromString(uiListElement.name);

					uiListElement.name = objectName + title;
					uiListElement.SetLabel(title);
				}
			}

			return uiListElement;
		}

		private T ExtractAttributeFromFieldInfo<T>(FieldInfo field) where T : Attribute
		{
			if(field != null)
			{
				foreach (object attr in field.GetCustomAttributes(typeof(T), false))
				{
					if(attr is T)
					{
						return attr as T;
					}
				}
			}

			return null;
		}

		private string CapitalizeFirstLetter(string originalText)
		{
			if(string.IsNullOrEmpty(originalText))
			{
				return null;
			}

			return originalText.First().ToString().ToUpper() + originalText.Substring(1);
		}

		private string RemoveCloneMarkerFromString(string originalText)
		{
			if (string.IsNullOrEmpty(originalText))
			{
				return null;
			}

			return originalText.Replace("(Clone)", string.Empty);
		}

		private void ProcessTextFieldAttribute(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			TextFieldAttribute textFieldAttr = ExtractAttributeFromFieldInfo<TextFieldAttribute>(field);

			if (textFieldAttr != null)
			{
				string fieldValue = string.Empty;
				object objValue = field.GetValue(templateObject);

				if (objValue != null)
				{
					fieldValue = objValue.ToString();
				}

				UITextField uiTextField = CreateUIComponent<UITextField>(uiTextFieldPrefab,
					fieldName, fieldValue, parent);

				if (textFieldAttr != null)
				{
					uiTextField.SetReadOnly(string.IsNullOrEmpty(fieldValue) ? false : textFieldAttr.readOnly);
					uiTextField.SetContentType(textFieldAttr.textFieldType);
				}

				if (addToForm)
				{
					uiFormInstance.AddUIComponent(uiTextField);
				}
			}
		}

		private void ProcessCheckBoxAttribute(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			CheckBoxAttribute checkBoxAttr = ExtractAttributeFromFieldInfo<CheckBoxAttribute>(field);

			if (checkBoxAttr != null)
			{
				string fieldValue = string.Empty;
				object objValue = field.GetValue(templateObject);

				if (objValue != null)
				{
					fieldValue = objValue.ToString();
				}

				UICheckBox uiCheckBox = CreateUIComponent<UICheckBox>(uiCheckBoxPrefab,
					fieldName, fieldValue, parent);

				if (addToForm)
				{
					uiFormInstance.AddUIComponent(uiCheckBox);
				}
			}
		}

		private void ProcessDropDown(FieldInfo field, string fieldName, RectTransform parent, bool addToForm)
		{
			DropDownAttribute dropDrownAttr = ExtractAttributeFromFieldInfo<DropDownAttribute>(field);

			if (dropDrownAttr != null)
			{
				UIDropDown uiDropDown = CreateUIComponent<UIDropDown>(uiDropDownPrefab,
					fieldName, string.Empty, parent);

				if (uiDropDown != null)
				{
					uiDropDown.ClearOptions();

					Type enumType = field.FieldType;
					Type enumUnderlyingType = Enum.GetUnderlyingType(enumType);
					Array enumValues = Enum.GetValues(enumType);

					foreach (var enumValue in enumValues)
					{
						//object underlyingValue = Convert.ChangeType(enumValue, enumUnderlyingType);
						uiDropDown.SetOption(enumValue.ToString());
					}
				}

				if (addToForm)
				{
					uiFormInstance.AddUIComponent(uiDropDown);
				}
			}
		}

		private void ProcessListElement(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			ListElementAttribute listElementAttr = ExtractAttributeFromFieldInfo<ListElementAttribute>(field);
			
			if (listElementAttr != null)
			{
				UIListElement uiListElement = CreateUIListElement(fieldName, parent);

				if (uiListElement != null)
				{					
					object listFieldValue = field.GetValue(templateObject);

					if (typeof(IList).IsAssignableFrom(field.FieldType))
					{
						IList list = listFieldValue as IList;
						Type listElementType = GetElementTypeOfEnumerable(field.FieldType);

						if (listElementType != null)
						{
							if (listElementType.IsPrimitive)
							{
								
							}
							else
							{
								uiListElement.TemplateObject = Activator.CreateInstance(listElementType);
							}
						}

						for (int i = 0; i < list.Count; i++)
						{
							var item = list[i];
							string itemName = fieldName;

							//Is using plural name?
							if ("S".Equals(fieldName.Last().ToString().ToUpper()))
							{
								itemName = itemName.Substring(0, fieldName.Length - 1) + "_" + (i + 1);
							}

							UITextField uiTextFieldListElement = CreateUIComponent<UITextField>(uiTextFieldPrefab,
								itemName, item.ToString(), uiListElement.listContent);

							if (uiTextFieldListElement != null)
							{
								uiTextFieldListElement.EnableDeleteButton();
							}

							uiListElement.AddComponent(uiTextFieldListElement);
						}
					}

					if (addToForm)
					{
						uiFormInstance.AddUIComponent(uiListElement);
					}
				}
			}
		}

		private static Type GetElementTypeOfEnumerable(Type type)
		{
			Type[] interfaces = type.GetInterfaces();

			return interfaces
				.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				.Select(x => x.GetGenericArguments()[0]).FirstOrDefault();
		}

		public void UpdateListElements()
		{
			if(uiFormInstance != null)
			{
				uiFormInstance.UpdateListElements();
			}
		}
	}
}