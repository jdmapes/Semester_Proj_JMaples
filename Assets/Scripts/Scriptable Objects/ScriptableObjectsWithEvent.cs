using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsWithEvent : MonoBehaviour
{
    private Dictionary<string, List<System.Action<object[]>>> listeners = new Dictionary<string, List<System.Action<object[]>>>();

    public void AddListener(string eventName, System.Action<object[]> callback)
    {
        if (!listeners.ContainsKey(eventName))
        {
            listeners.Add(eventName, new List<System.Action<object[]>>());
        }
        listeners[eventName].Add(callback);
    }

    public void RemoveListener(string eventName, System.Action<object[]> callback)
    {
        if (!listeners.ContainsKey(eventName)) return;
        listeners[eventName].RemoveAll(m => m == callback);

        if (listeners[eventName].Count == 0) listeners.Remove(eventName);
    }

    public void SendEvent(string eventName, object[] parameters)
    {
        if (listeners.TryGetValue(eventName, out List<System.Action<object[]>> actions))
        {
            for (var i = 0; i < actions.Count; i++)
            {
                try
                {
                    actions[i].Invoke(parameters);
                }
                catch (System.Exception error)
                {
                    Debug.LogError(error);
                }
            }
        }
    }
}
