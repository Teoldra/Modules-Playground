using UnityEngine;
using UnityEngine.Events;

public class FloatGameEventListener : MonoBehaviour
{
    [SerializeField] private FloatGameEvent gameEvent;
    [SerializeField] private UnityEvent<float> response;

    private void OnEnable() => gameEvent?.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent?.UnregisterListener(OnEventRaised);

    private void OnEventRaised(float value) => response?.Invoke(value);
}
