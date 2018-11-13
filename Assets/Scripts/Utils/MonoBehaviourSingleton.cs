using UnityEngine;
using System.Collections;

public class MonoBehaviourSingleton<T> : MonoBehaviour
	where T : Component
{
	private static T _instance;

	public static void SetInstanceInternally(T theInst) {
		_instance = theInst;
	}

	public static T Instance {
		get {
			if (_instance == null) {
				var objs = FindObjectsOfType (typeof(T)) as T[];
				if (objs.Length > 0)
					_instance = objs[0];
				if (objs.Length > 1) {
					Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
				}
				if (_instance == null) {
					Debug.LogWarning("There is no instance of " + typeof(T).Name + " in the scene.");
				}
			}
			return _instance;
		}
	}
}


public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
	where T : Component
{
	public static T Instance { get; private set; }
	
	public virtual void Initialize() {
	}

	public virtual void Awake ()
	{
		if (Instance == null) {
			Instance = this as T;
			Initialize ();
			DontDestroyOnLoad (this);
		} else {
			Debug.LogWarning ("There was an existing persistent " + typeof(T).Name +
			" in the current scene. Deleted the one in the scene as it should only use the first one created.");
			Destroy (gameObject);
		}
	}
}