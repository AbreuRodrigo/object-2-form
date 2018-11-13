using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UIForm : MonoBehaviour
	{
		[SerializeField]
		private Text formHeader;

		[SerializeField]
		private RectTransform formBody;

		public List<UITextField> textFields;
		public List<UIDropDown> dropDowns;
		public List<UIListElement> lists;
		public List<UICheckBox> checkBoxes;

		private object referenceObject;

		public RectTransform FormBody { get { return formBody; } }

		public void SetTitleText(string titleText)
		{
			if (formHeader != null)
			{
				formHeader.text = titleText;
			}
		}

		public void SetReferenceObject(object referenceObject)
		{
			this.referenceObject = referenceObject;
		}

		public void AddUIComponent(UITextField uiTextField)
		{
			if (textFields == null)
			{
				textFields = new List<UITextField>();
			}

			if(textFields != null)
			{
				textFields.Add(uiTextField);
			}
		}

		public void AddUIComponent(UIDropDown uiDropDown)
		{
			if (dropDowns == null)
			{
				dropDowns = new List<UIDropDown>();
			}

			if (dropDowns != null)
			{
				dropDowns.Add(uiDropDown);
			}
		}

		public void AddUIComponent(UIListElement uiListElement)
		{
			if (lists == null)
			{
				lists = new List<UIListElement>();
			}

			if (lists != null)
			{
				lists.Add(uiListElement);
			}
		}

		public void AddUIComponent(UICheckBox uiCheckBox)
		{
			if (checkBoxes == null)
			{
				checkBoxes = new List<UICheckBox>();
			}

			if (checkBoxes != null)
			{
				checkBoxes.Add(uiCheckBox);
			}
		}

		public void AddUIComponent(UIComponent uiComponent)
		{
			if(uiComponent != null)
			{
				if(uiComponent is UITextField)
				{
					AddUIComponent(uiComponent as UITextField);
				}
				else if(uiComponent is UIDropDown)
				{
					AddUIComponent(uiComponent as UIDropDown);
				}
				else if (uiComponent is UICheckBox)
				{
					AddUIComponent(uiComponent as UICheckBox);
				}
				else if (uiComponent is UIListElement)
				{
					AddUIComponent(uiComponent as UIListElement);
				}
			}
		}

		public void UpdateListElements()
		{
			if (lists != null)
			{
				foreach (UIListElement list in lists)
				{
					list.UpdateRect();
				}

				formBody.ForceUpdateRectTransforms();
				Canvas.ForceUpdateCanvases();
			}
		}
	}
}