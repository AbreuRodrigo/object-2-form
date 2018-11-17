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
		public UIObject uiObjectPrefab;

		[Header("Components")]
		public UIComponentFactory uiFactory;

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
				//Current UI Form
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

				fieldName = Utils.ClearGenericNaming(fieldName);
				fieldName = Utils.CapitalizeFirstLetter(fieldName);

				ProcessTextFieldAttribute(field, fieldName, templateObject, parent, addToForm);
				ProcessCheckBoxAttribute(field, fieldName, templateObject, parent, addToForm);
				ProcessDropDown(field, fieldName, templateObject, parent, addToForm);
				ProcessObjectField(field, fieldName, templateObject, parent, addToForm);
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
					uiForm.name = Utils.RemoveCloneMarkerFromString(uiForm.name) + title;
					uiForm.SetTitleText(title);
				}
			}

			return uiForm;
		}

		private UIListElement CreateUIListElement(string title, Transform parent)
		{
			UIListElement uiListElement = null;

			if(uiListElementPrefab != null)
			{
				uiListElement = Instantiate(uiListElementPrefab, parent);

				if(uiListElement != null)
				{
					string objectName = Utils.RemoveCloneMarkerFromString(uiListElement.name);

					uiListElement.name = objectName + title;
					uiListElement.SetLabel(title);
				}
			}

			return uiListElement;
		}

		private void ProcessTextFieldAttribute(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			TextFieldAttribute textFieldAttr = Utils.ExtractAttributeFromFieldInfo<TextFieldAttribute>(field);

			if (textFieldAttr != null)
			{
				object objValue = field?.GetValue(templateObject);
				string fieldValue = objValue?.ToString();

				UITextField uiTextField = uiFactory?.CreateUI<UITextField>(uiTextFieldPrefab, fieldName, fieldValue, parent);

				if (textFieldAttr != null)
				{
					uiTextField?.SetReadOnly(string.IsNullOrEmpty(fieldValue) ? false : textFieldAttr.readOnly);
					uiTextField?.SetContentType(textFieldAttr.textFieldType);
				}

				if (addToForm)
				{
					uiFormInstance?.AddUIComponent(uiTextField);
				}
			}
		}

		private void ProcessCheckBoxAttribute(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			CheckBoxAttribute checkBoxAttr = Utils.ExtractAttributeFromFieldInfo<CheckBoxAttribute>(field);

			if (checkBoxAttr != null)
			{
				object objValue = field?.GetValue(templateObject);
				string fieldValue = objValue?.ToString();

				UICheckBox uiCheckBox = uiFactory?.CreateUI<UICheckBox>(uiCheckBoxPrefab, fieldName, fieldValue, parent);

				if (addToForm)
				{
					uiFormInstance?.AddUIComponent(uiCheckBox);
				}
			}
		}

		private void ProcessDropDown(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			DropDownAttribute dropDrownAttr = Utils.ExtractAttributeFromFieldInfo<DropDownAttribute>(field);

			if (dropDrownAttr != null)
			{				
				object item = field?.GetValue(templateObject);
				string fieldValue = item?.ToString();

				CreateUIDropDown(field.FieldType, fieldValue, fieldName, templateObject, parent, addToForm);
			}
		}

		private void ProcessObjectField(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			ObjectFieldAttribute listElementAttr = Utils.ExtractAttributeFromFieldInfo<ObjectFieldAttribute>(field);

			if (listElementAttr != null)
			{
				object item = field?.GetValue(templateObject);

				ProcessObjectField(item, fieldName, field.FieldType, parent);
			}
		}

		private void ProcessListElement(FieldInfo field, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			ListElementAttribute listElementAttr = Utils.ExtractAttributeFromFieldInfo<ListElementAttribute>(field);
			
			if (listElementAttr != null)
			{
				UIListElement uiListElement = CreateUIListElement(fieldName, parent);

				if (uiListElement != null)
				{					
					object listFieldValue = field.GetValue(templateObject);

					if (typeof(IList).IsAssignableFrom(field.FieldType))
					{
						IList list = listFieldValue as IList;
						Type listElementType = Utils.GetElementTypeOfEnumerable(field.FieldType);

						if (listElementType != null)
						{
							if (listElementType == typeof(String))
							{
								uiListElement.TemplateObject = null;
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

							if ("S".Equals(fieldName.Last().ToString().ToUpper()))
							{
								itemName = itemName.Substring(0, fieldName.Length - 1) + "_" + (i + 1);
							}

							ProcessObjectField(item, itemName, listElementType, uiListElement.listContent);

							//ConvertObjectToFormUIs(item, uiListElement.listContent, false);
							//if (uiTextFieldListElement != null)
							//{
							//	uiTextFieldListElement.EnableDeleteButton();
							//}
							//uiListElement.AddComponent(uiTextFieldListElement);
						}
					}

					if (addToForm)
					{
						uiFormInstance?.AddUIComponent(uiListElement);
					}
				}
			}
		}

		private void ProcessObjectField(object item, string fieldName, Type objectType, RectTransform parent)
		{
			Type typeBinder = TypeUIComponentBinder.GetTypeBySystemType(objectType);

			if (typeBinder == typeof(UITextField))
			{
				UITextField uiTextFieldListElement = uiFactory?.CreateUI<UITextField>(uiTextFieldPrefab,
					fieldName, item.ToString(), parent);

				//uiListElement?.AddComponent(uiTextFieldListElement);

				uiTextFieldListElement?.EnableDeleteButton();
			}
			else if (typeBinder == typeof(UICheckBox))
			{
			}
			else if (objectType.IsEnum)
			{
				UIDropDown dropDown = CreateUIDropDown(item?.GetType(), item?.ToString(), fieldName, item, parent, false);
				//uiListElement?.AddComponent(dropDown);
			}
			else //When it's an internal object in the form
			{
				UIObject uiObjectInstance = uiFactory?.CreateUI<UIObject>(uiObjectPrefab, fieldName, parent);

				//uiListElement?.AddComponent(uiObjectInstance);

				ConvertObjectToFormUIs(item, uiObjectInstance.rectTransform, false);

				uiObjectInstance?.EnableDeleteButton();
			}
		}

		public void UpdateListElements()
		{
			uiFormInstance?.UpdateListElements();
		}

		private UIDropDown CreateUIDropDown(Type fieldType, string fieldValue, string fieldName, object templateObject, RectTransform parent, bool addToForm)
		{
			UIDropDown dropDown = uiFactory?.CreateUI<UIDropDown>(uiDropDownPrefab, fieldName, parent);
			dropDown?.ClearOptions();
			dropDown?.CreateOptions(fieldType);
			dropDown?.SetSelection(fieldValue);

			if (addToForm)
			{
				uiFormInstance?.AddUIComponent(dropDown);
			}

			return dropDown;
		}
	}
}