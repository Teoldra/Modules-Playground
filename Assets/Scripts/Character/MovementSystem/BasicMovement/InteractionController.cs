using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour, IInteractInput
{
    [Header("Interacting")]
    [SerializeField] Camera playerCamera;
    [SerializeField] private bool realeasedInteractButton = true;
    [SerializeField] private bool interacting;
    [SerializeField] private float interactionDistance;
    IInteractable currentTargetInteractable;
    [SerializeField] TextMeshProUGUI interactionText;

    void Update()
    {
        UpdateCurrentInteractable();
        UpdateInteractionText();
        Interact();
    }
    public void IsInteracting(bool interact)
    {
        interacting = interact;
        if (!interact)
        {
            realeasedInteractButton = true;
        }
    }
    private void UpdateCurrentInteractable()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        Physics.Raycast(ray, out var hit, interactionDistance);

        if (hit.collider != null)
        {
            currentTargetInteractable = hit.collider.GetComponent<IInteractable>();
        }
    }
    private void UpdateInteractionText()
    {
        if (currentTargetInteractable == null)
        {
            interactionText.text = string.Empty;
            return;
        }
        interactionText.text = currentTargetInteractable.InteractMessage;
    }
    private void Interact()
    {
        if (interacting && realeasedInteractButton && currentTargetInteractable != null)
        {
            currentTargetInteractable.Interact();
            realeasedInteractButton = false;
        }
    }
}
