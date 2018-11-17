using System;
using System.Linq;
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

		public void CreateOptions(Type type)
		{
			foreach (var e in Enum.GetValues(type))
			{
				SetOption(e.ToString());
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

		public void SetSelection(string value)
		{
			if(dropdown != null)
			{
				int index = 0;
				
				foreach(Dropdown.OptionData opt in dropdown.options)
				{
					if(opt.text.Equals(value))
					{
						break;
					}

					index++;
				}

				dropdown.value = index;
			}
		}
	}
}