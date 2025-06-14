using UnityEngine;

namespace Core.Patterns
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField] private bool dontDestroy;
		private static T _instance;
		private static readonly object _instanceLock = new object();
		private static bool _quitting = false;

		public static T Instance
		{
			get
			{
				lock (_instanceLock)
				{
					if (_instance == null && !_quitting)
					{
						_instance = GameObject.FindObjectOfType<T>();
					}

					return _instance;
				}
			}
		}

		protected virtual void Awake()
		{
			UnityEngine.Debug.Log($"[SINGLETON] awake base {gameObject} started");
			if (_instance == null)
			{
				_instance = gameObject.GetComponent<T>();
				if (dontDestroy) DontDestroyOnLoad(_instance.gameObject);
			}
			else if (_instance)
			{
				if (dontDestroy) DontDestroyOnLoad(_instance.gameObject);
			}
			else if (_instance.GetInstanceID() != GetInstanceID())
			{
				Debug.LogWarning($"Removing duplicate.");
				Destroy(this);
			}
			
			UnityEngine.Debug.Log($"[SINGLETON] awake base {gameObject} finished");
		}

		protected virtual void OnDisable()
		{
			UnityEngine.Debug.Log($"[SINGLETON] game object {gameObject.name} is disabled");
		}
		
		protected virtual void OnApplicationQuit()
		{
			_quitting = true;
		}
	}
}