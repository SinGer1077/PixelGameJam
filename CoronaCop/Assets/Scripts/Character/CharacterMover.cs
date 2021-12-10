using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private float _characterSpeed;

    [SerializeField]
    private Rigidbody _rigidBody;

    private void Update()
    {
        _rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal") * _characterSpeed, _rigidBody.velocity.y, Input.GetAxis("Vertical") * _characterSpeed);
    }
}
