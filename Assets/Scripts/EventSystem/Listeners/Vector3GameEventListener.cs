using UnityEngine;
using UnityEngine.Events;

public class Vector3GameEventListener : MonoBehaviour
{
    [SerializeField] private Vector3GameEvent gameEvent;
    [SerializeField] private UnityEvent<Vector3> response;

    private void OnEnable() => gameEvent?.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent?.UnregisterListener(OnEventRaised);

    private void OnEventRaised(Vector3 value) => response?.Invoke(value);
}
