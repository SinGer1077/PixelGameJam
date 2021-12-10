using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _characterTrasform;

    [SerializeField]
    private Rigidbody _characterRigidBody;

    private void Update()
    {
        _characterTrasform.rotation.SetLookRotation(_characterRigidBody.velocity);
    }
}
