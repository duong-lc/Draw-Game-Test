using System;
using System.Collections.Generic;
using Core.Patterns;
using UnityEngine;

namespace Core.Events
{
	public class EventDispatcher : Singleton<EventDispatcher>
	{
		private Dictionary<EventType, Action<object>> persistentEvents = new();
		private Dictionary<EventType, Action<object>> Events = new();

		/// <summary>
		/// Add a listener to an event type.
		/// </summary>
		/// <param name="eventType">Type of event.</param>
		/// <param name="callback">Action to trigger on event.</param>
		public void AddListener(EventType eventType, Action<object> callback, bool isPersistent)
		{
			if (isPersistent)
			{
				if (persistentEvents.ContainsKey(eventType))
				{
					persistentEvents[eventType] += callback;
				}
				else
				{
					persistentEvents.Add(eventType, callback);
				}

				return;
			}
			
			if (Events.ContainsKey(eventType))
			{
				Events[eventType] += callback;
			}
			else
			{
				Events.Add(eventType, callback);
			}
		}

		/// <summary>
		/// Remove a listener from subscribing to an event.
		/// </summary>
		/// <param name="eventType">Type of event</param>
		/// <param name="callback"></param>
		public void RemoveListener(EventType eventType, Action<object> callback, bool isPersistent)
		{
			if (Events.ContainsKey(eventType))
			{
				Events[eventType] -= callback;
			}

			if (isPersistent)
			{
				if (persistentEvents.ContainsKey(eventType))
				{
					persistentEvents[eventType] -= callback;
				}
			}
		}

		/// <summary>
		/// Trigger an event and its callbacks with parameters.
		/// </summary>
		/// <param name="eventType">Event to trigger.</param>
		/// <param name="param">Paramaters</param>
		public void FireEvent(EventType eventType, object param = null)
		{
			if (Events.ContainsKey(eventType))
			{
				var actions = Events[eventType];
				if (actions == null)
				{
					Events.Remove(eventType);
					return;
				}
				
				UnityEngine.Debug.Log($"[Event_Dispatcher] Executing event {eventType} with {actions.GetInvocationList().Length} invocations.");
				try
				{
					actions.Invoke(param);
				} catch (Exception e)
				{
					UnityEngine.Debug.LogError($"Error when execute callback in dispatcher: {e}");
				}
			}
			
			if (persistentEvents.ContainsKey(eventType))
			{
				var actions = persistentEvents[eventType];
				if (actions == null)
				{
					persistentEvents.Remove(eventType);
					return;
				}
				
				UnityEngine.Debug.Log($"[Event_Dispatcher] Executing event {eventType} with {actions.GetInvocationList().Length} invocations.");
				try
				{
					actions.Invoke(param);
				} catch (Exception e)
				{
					UnityEngine.Debug.LogError($"Error when execute callback in dispatcher: {e}");
				}
			}
		}

		/// <summary>
		/// Clear all listeners subscribed to an event type.
		/// </summary>
		/// <param name="eventType">Type of event</param>
		public void ClearListeners(EventType eventType, bool isPersistent = false)
		{
			if (Events.ContainsKey(eventType))
			{
				Events[eventType] = null;
			}

			if (isPersistent)
			{
				if (persistentEvents.ContainsKey(eventType))
				{
					persistentEvents[eventType] = null;
				}
			}
		}

		public void ClearListeners(bool isPersistent = false)
		{
			Events.Clear();
			if(isPersistent)
				persistentEvents.Clear();
		}
	}

	/// <summary>
	/// Extensions to help with calling event-related methods.
	/// </summary>
	public static class EventDispatcherExtension
	{
		public static void AddListener(this MonoBehaviour listener, EventType eventType, Action<object> callback, bool isPersistent = false)
		{
			EventDispatcher.Instance.AddListener(eventType, callback, isPersistent);
		}

		public static void RemoveListener(this MonoBehaviour listener, EventType eventType, Action<object> callback, bool isPersistent = false)
		{
			EventDispatcher.Instance.RemoveListener(eventType, callback, isPersistent);
		}

		public static void FireEvent(this MonoBehaviour listener, EventType eventType, object param = null)
		{
			EventDispatcher.Instance.FireEvent(eventType, param);
		}

		// public static void AddListener(this object listener, EventType eventType, Action<object> callback)
		// {
		// 	EventDispatcher.Instance.AddListener(eventType, callback);
		// }
		//
		// public static void RemoveListener(this object listener, EventType eventType, Action<object> callback)
		// {
		// 	EventDispatcher.Instance.RemoveListener(eventType, callback);
		// }

		public static void FireEvent(this object listener, EventType eventType, object param = null)
		{
			EventDispatcher.Instance.FireEvent(eventType, param);
		}
	}
}