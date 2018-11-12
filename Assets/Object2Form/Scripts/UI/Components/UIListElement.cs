using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UIListElement : UIComponent
	{
		public Text listTitle;
		public RectTransform listContent;
		public Button addButton;

		private void Start()
		{
			if(addButton != null)
			{
				addButton.onClick.AddListener(OnAddElement);
			}
		}

		public void AddComponent(UIComponent component)
		{
			if (component != null)
			{
				component.transform.SetParent(listContent);
			}
		}

		private void OnAddElement()
		{

		}
	}
}