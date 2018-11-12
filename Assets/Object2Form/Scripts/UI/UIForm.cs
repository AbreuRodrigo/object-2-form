using UnityEngine;

namespace O2F
{
	public class UIForm : MonoBehaviour
	{
		[SerializeField]
		private RectTransform formContent;

		[SerializeField]
		private UIFormTitle formTitle;

		private object referenceObject;

		public RectTransform FormContent { get { return formContent; } }

		public void SetFormTitle(UIFormTitle formTitle)
		{
			this.formTitle = formTitle;
		}

		public void SetReferenceObject(object referenceObject)
		{
			this.referenceObject = referenceObject;
		}
	}
}