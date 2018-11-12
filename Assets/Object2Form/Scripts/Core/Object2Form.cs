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
		private static Object2Form Instance { get; set; }

		[Header("Prefabs")]
		public UIForm uiFormPrefab;
		public UIFormTitle uiFormTitlePrefab;
		public UITextField uiTextFieldPrefab;
		public UICheckBox uiCheckBoxPrefab;
		public UIDropDown uiDropDownPrefab;
		public UIListElement uiListElementPrefab;

		private void Awake()
		{
			Instance = this;
		}

		public static void CreateEditForm(object sourceObject)
		{
			if (sourceObject != null)
			{
				FieldInfo[] fields = sourceObject.GetType().GetFields(
					BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

				UIForm uiForm = Instance.CreateUIForm(sourceObject.GetType().Name);

				if (uiForm != null)
				{
					uiForm.SetReferenceObject(sourceObject);

					//All the fields in the sourceObject
					foreach (FieldInfo field in fields)
					{
						string fieldName = field.Name;
						
						fieldName = fieldName.Replace("<", string.Empty);
						fieldName = fieldName.Replace(">", string.Empty);

						fieldName = Instance.CapitalizeFirstLetter(fieldName);

						ProcessTextFieldAttribute(field, fieldName, sourceObject, uiForm.FormContent);
						ProcessCheckBoxAttribute(field, fieldName, sourceObject, uiForm.FormContent);
						ProcessDropDown(field, fieldName, uiForm.FormContent);
						ProcessListElement(field, fieldName, sourceObject, uiForm.FormContent);
					}
				}
			}
		}

		private UIForm CreateUIForm(string title)
		{
			UIForm uiForm = null;

			if(uiFormPrefab != null)
			{
				uiForm = Instantiate(uiFormPrefab, transform);

				if (!string.IsNullOrEmpty(title) && uiFormTitlePrefab != null && uiForm != null)
				{
					uiForm.name = RemoveCloneMarkerFromString(uiForm.name) + title;

					UIFormTitle uiFormTitle = Instantiate(uiFormTitlePrefab, uiForm.FormContent);

					uiFormTitle.name = RemoveCloneMarkerFromString(uiFormTitle.name);

					uiFormTitle.SetTitleText(title);
					uiForm.SetFormTitle(uiFormTitle);
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

		private static void ProcessTextFieldAttribute(FieldInfo field, string fieldName, object sourceObject, RectTransform parent)
		{
			TextFieldAttribute textFieldAttr = Instance.ExtractAttributeFromFieldInfo<TextFieldAttribute>(field);

			if (textFieldAttr != null)
			{
				string fieldValue = field.GetValue(sourceObject).ToString();

				UITextField uiTextField = Instance.CreateUIComponent<UITextField>(Instance.uiTextFieldPrefab,
					fieldName, fieldValue, parent);

				if (textFieldAttr != null)
				{
					uiTextField.SetReadOnly(textFieldAttr.readOnly);
					uiTextField.SetContentType(textFieldAttr.textFieldType);
				}
			}
		}

		private static void ProcessCheckBoxAttribute(FieldInfo field, string fieldName, object sourceObject, RectTransform parent)
		{
			CheckBoxAttribute checkBoxAttr = Instance.ExtractAttributeFromFieldInfo<CheckBoxAttribute>(field);

			if (checkBoxAttr != null)
			{
				string fieldValue = field.GetValue(sourceObject).ToString();

				UICheckBox uiCheckBox = Instance.CreateUIComponent<UICheckBox>(Instance.uiCheckBoxPrefab,
					fieldName, fieldValue, parent);
			}
		}

		private static void ProcessDropDown(FieldInfo field, string fieldName, RectTransform parent)
		{
			DropDownAttribute dropDrownAttr = Instance.ExtractAttributeFromFieldInfo<DropDownAttribute>(field);

			if (dropDrownAttr != null)
			{
				UIDropDown uiDropDown = Instance.CreateUIComponent<UIDropDown>(Instance.uiDropDownPrefab,
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
			}
		}

		private static void ProcessListElement(FieldInfo field, string fieldName, object sourceObject, RectTransform parent)
		{
			ListElementAttribute listElementAttr = Instance.ExtractAttributeFromFieldInfo<ListElementAttribute>(field);
			
			if (listElementAttr != null)
			{
				UIListElement uiListElement = Instance.CreateUIListElement(fieldName, parent);

				if (uiListElement != null)
				{
					var listFieldValue = field.GetValue(sourceObject);

					if (typeof(IList).IsAssignableFrom(field.FieldType))
					{
						IList list = listFieldValue as IList;

						for (int i = 0; i < list.Count; i++)
						{
							var item = list[i];
							var itemType = item.GetType();

							string itemName = fieldName;

							//Is using plural name?
							if ("S".Equals(fieldName.Last().ToString().ToUpper()))
							{
								itemName = itemName.Substring(0, fieldName.Length - 1) + "_" + (i + 1);
							}

							UITextField uiTextFieldListElement = Instance.CreateUIComponent<UITextField>(Instance.uiTextFieldPrefab,
								itemName, item.ToString(), uiListElement.listContent);

							if (uiTextFieldListElement != null)
							{
								uiTextFieldListElement.EnableDeleteButton();
							}
						}
					}
				}
			}
		}
	}
}