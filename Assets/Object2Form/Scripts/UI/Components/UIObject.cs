using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UIObject : UIComponent
	{
		[SerializeField]
		public RectTransform rectTransform;

		[SerializeField]
		private Button deleteButton;

		private void Awake()
		{
			DisableDeleteButton();

			if (deleteButton != null)
			{
				deleteButton.onClick.AddListener(OnDeleteInList);
			}
		}

		public void EnableDeleteButton()
		{
			if (deleteButton != null)
			{
				deleteButton.gameObject.SetActive(true);
			}
		}

		public void DisableDeleteButton()
		{
			if (deleteButton != null)
			{
				deleteButton.gameObject.SetActive(false);
			}
		}

		private void OnDeleteInList()
		{
			TweenManager.Instance.ValueTransition(1, 0, 0.5f, TweenType.BackIn, true, null, (float v) => {
				Vector3 scale = gameObject.transform.localScale;
				scale.y = v;
				gameObject.transform.localScale = scale;
			}, () => {
				Destroy(gameObject);
				Object2Form.Instance.UpdateListElements();
			});
		}
	}
}