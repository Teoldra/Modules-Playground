using UnityEngine;
using UnityEngine.Events;

public class StringGameEventListener : MonoBehaviour
{
    [SerializeField] private StringGameEvent gameEvent;
    [SerializeField] private UnityEvent<string> response;

    private void OnEnable() => gameEvent?.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent?.UnregisterListener(OnEventRaised);

    private void OnEventRaised(string value) => response?.Invoke(value);
}
