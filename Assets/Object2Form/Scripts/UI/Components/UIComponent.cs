using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public abstract class UIComponent : MonoBehaviour
	{
		protected const string READ_ONLY = "<color=#999> [read only] </color>";

		[SerializeField]
		protected Text label;

		[SerializeField]
		protected RectTransform labelContainer;

		[SerializeField]
		protected Selectable selectableComponent;

		public virtual void SetValue(string value) { }

		public void EnableLabel()
		{
			if (labelContainer != null)
			{
				labelContainer.gameObject.SetActive(true);
			}
		}

		public void DisableLabel()
		{
			if(labelContainer != null)
			{
				labelContainer.gameObject.SetActive(false);
			}
		}

		public void SetLabel(string labelText)
		{
			if (label != null)
			{
				if (string.IsNullOrEmpty(labelText))
				{
					DisableLabel();
				}
				else
				{
					label.text = labelText;
					EnableLabel();
				}
			}
		}

		public void SetReadOnly(bool readOnly)
		{
			if (selectableComponent != null)
			{
				selectableComponent.interactable = !readOnly;

				if (label != null)
				{
					if (readOnly)
					{
						SetLabel(label.text + READ_ONLY);
					}
					else
					{
						SetLabel(label.text.Replace(READ_ONLY, string.Empty));
					}
				}
			}
		}
	}
}