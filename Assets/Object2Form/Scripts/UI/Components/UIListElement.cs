using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UIListElement : UIComponent
	{
		public Text listTitle;
		public Button addButton;
		public RectTransform listContent;
		public RectTransform rectTransform;

		private Vector2 previousContentSize;

		private void Start()
		{
			if(addButton != null)
			{
				addButton.onClick.AddListener(OnAddElement);
			}

			UpdatePreviousContentSizeDelta();
		}

		public void AddComponent(UIComponent component)
		{
			if (component != null)
			{
				component.transform.SetParent(listContent);
			}

			UpdatePreviousContentSizeDelta();
		}

		private void OnAddElement()
		{

		}

		public void UpdateRect()
		{
			listContent.sizeDelta = listContent.sizeDelta;

			Vector2 diff = previousContentSize - listContent.sizeDelta;
			rectTransform.sizeDelta += diff;
			//rectTransform.ForceUpdateRectTransforms();

			UpdatePreviousContentSizeDelta();
		}

		private void UpdatePreviousContentSizeDelta()
		{
			previousContentSize = listContent.sizeDelta;
		}
	}
}