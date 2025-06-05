using UnityEngine.Events;
using UnityEngine;

public abstract class GameEvent<T> : ScriptableObject
{
    private UnityEvent<T> listeners = new UnityEvent<T>();

    public void Raise(T value)
    {
        listeners.Invoke(value);
    }

    public void RegisterListener(UnityAction<T> listener)
    {
        listeners.AddListener(listener);
    }

    public void UnregisterListener(UnityAction<T> listener)
    {
        listeners.RemoveListener(listener);
    }
}