using UnityEngine;
using UnityEngine.Events;

public class GameObjectGameEventListener : MonoBehaviour
{
    [SerializeField] private GameObjectGameEvent gameEvent;
    [SerializeField] private UnityEvent<GameObject> response;

    private void OnEnable() => gameEvent?.RegisterListener(OnEventRaised);
    private void OnDisable() => gameEvent?.UnregisterListener(OnEventRaised);

    private void OnEventRaised(GameObject value) => response?.Invoke(value);
}
