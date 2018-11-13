using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UITextField : UIComponent
	{		
		[SerializeField]
		private InputField inputField;

		[SerializeField]
		private Button deleteButton;

		private void Awake()
		{
			DisableDeleteButton();

			if(deleteButton != null)
			{
				deleteButton.onClick.AddListener(OnDeleteInList);
			}
		}

		public override void SetValue(string text)
		{
			if (inputField != null)
			{
				inputField.text = text;
			}
		}

		public void SetInitialText(string initialText)
		{
			if(inputField != null)
			{
				Text placeholder = inputField.placeholder.GetComponent<Text>();

				if(placeholder != null)
				{
					placeholder.text = initialText;
				}
			}
		}

		public void SetContentType(ETextFieldType type)
		{
			if(inputField != null)
			{
				InputField.ContentType contentType = InputField.ContentType.Standard;

				switch (type)
				{
					case ETextFieldType.String:
						contentType = InputField.ContentType.Standard;
						break;
					case ETextFieldType.Short:						
					case ETextFieldType.Integer:
						contentType = InputField.ContentType.IntegerNumber;
						break;
					case ETextFieldType.Float:
					case ETextFieldType.Double:
						contentType = InputField.ContentType.DecimalNumber;
						break;
				}

				inputField.contentType = contentType;
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
			}, ()=> {
				Destroy(gameObject);
				Object2Form.Instance.UpdateListElements();
			});						
		}
	}
}