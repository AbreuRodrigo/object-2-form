using UnityEngine;

namespace O2F
{
	public class UIComponentFactory : MonoBehaviour
	{
		public T CreateUI<T>(Component prefab, string label, Transform parent) where T : UIComponent
		{
			return CreateUI<T>(prefab, label, null, parent);
		}

		public T CreateUI<T>(Component prefab, string label, string value, Transform parent) where T : UIComponent
		{
			T uiComponent = null;

			if (prefab != null)
			{
				uiComponent = Instantiate(prefab, parent).GetComponent<T>();

				if (uiComponent != null)
				{
					string objectName = Utils.RemoveCloneMarkerFromString(uiComponent.name);

					uiComponent.name = objectName + label;
					uiComponent.SetLabel(label);
					uiComponent.SetValue(value);
					uiComponent.SetReadOnly(false);
				}
			}

			return uiComponent;
		}
	}
}