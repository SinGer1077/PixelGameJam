using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private float _characterSpeed;

    public Vector3 _movementDirection;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontal, 0, vertical);
        _movementDirection.Normalize();

        transform.Translate(_movementDirection * _characterSpeed * Time.deltaTime, Space.World);

    }
}
