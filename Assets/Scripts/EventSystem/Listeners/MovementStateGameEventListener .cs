using UnityEngine.Events;
using UnityEngine;

public class MovementStateGameEventListener : MonoBehaviour
{
    [SerializeField] private MovementStateGameEvent gameEvent;
    [SerializeField] private UnityEvent<MovementState> response;

    private void OnEnable() => gameEvent.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent.UnregisterListener(OnEventRaised);
    private void OnEventRaised(MovementState state) => response?.Invoke(state);
}
