using UnityEngine;
using UnityEngine.Events;

public class IntGameEventListener : MonoBehaviour
{
    [SerializeField] private IntGameEvent gameEvent;
    [SerializeField] private UnityEvent<int> response;

    private void OnEnable() => gameEvent?.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent?.UnregisterListener(OnEventRaised);

    private void OnEventRaised(int value) => response?.Invoke(value);
}
