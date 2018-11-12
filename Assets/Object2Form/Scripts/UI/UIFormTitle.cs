using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UIFormTitle : MonoBehaviour
	{
		[SerializeField]
		private Text textTitle;

		public void SetTitleText(string titleText)
		{
			if(textTitle != null)
			{
				textTitle.text = titleText;
			}
		}
	}
}