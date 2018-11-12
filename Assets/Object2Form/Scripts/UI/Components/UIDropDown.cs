using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UIDropDown : UIComponent
	{
		[SerializeField]
		private Dropdown dropdown;
		
		public void ClearOptions()
		{
			if (dropdown != null)
			{
				dropdown.ClearOptions();
			}
		}

		public void SetOption(string optionValue)
		{
			if(dropdown != null)
			{
				Dropdown.OptionData option = new Dropdown.OptionData(optionValue);
				dropdown.options.Add(option);
			}
		}
	}
}