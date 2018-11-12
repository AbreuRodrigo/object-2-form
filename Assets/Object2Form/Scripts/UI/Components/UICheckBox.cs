using UnityEngine;
using UnityEngine.UI;

namespace O2F
{
	public class UICheckBox : UIComponent
	{
		[SerializeField]
		private Toggle toggle;

		public override void SetValue(string value)
		{
			bool check = false;
			bool.TryParse(value, out check);

			if(toggle != null)
			{
				toggle.isOn = check;
			}
		}
	}
}