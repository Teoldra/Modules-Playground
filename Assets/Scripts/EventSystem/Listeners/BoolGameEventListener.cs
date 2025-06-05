using UnityEngine;
using UnityEngine.Events;

public class BoolGameEventListener : MonoBehaviour
{
    [SerializeField] private BoolGameEvent gameEvent;
    [SerializeField] private UnityEvent<bool> response;

    private void OnEnable() => gameEvent?.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent?.UnregisterListener(OnEventRaised);

    private void OnEventRaised(bool value) => response?.Invoke(value);
}
