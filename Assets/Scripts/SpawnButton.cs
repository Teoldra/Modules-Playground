using System;
using UnityEngine;

public class SpawnButton : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] string interactMessage;

    public string InteractMessage => interactMessage;

    public void Interact()
    {
        Spawn();
    }

    private void Spawn()
    {
        var spawnedObject = Instantiate(spawnObject, transform.position + Vector3.left, Quaternion.identity);
    }
}
