using UnityEngine;


[CreateAssetMenu(fileName = "New CharController", menuName = "Assets/CharController")]
public class PlayerControllerSO : ScriptableObject
{
    public CharacterController CharacterController;
    public PlayerEvents Events;
}
