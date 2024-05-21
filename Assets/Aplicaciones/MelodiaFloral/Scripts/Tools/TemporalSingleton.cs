using UnityEngine;
using System.Collections;

public class TemporalSingleton<T> : MonoBehaviour where T : Component
{
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
				if (_instance == null)
				{
					GameObject obj = new GameObject();
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	public virtual void Awake()
	{
		if (_instance == null) { _instance = this as T;	}
		else if (_instance != this)	{ 
			Debug.LogWarning("Destroying duplicate TemporalSingleton"); 
			Destroy(gameObject);
		}
	}

	private static T _instance;
}