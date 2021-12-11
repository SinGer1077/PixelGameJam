using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _characterTrasform;

    [SerializeField]
    private CharacterMover _mover;

    [SerializeField]
    private float _rotationSpeed;

    private float _timeCount = 0;

    private void Update()
    {
        if (_mover._movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_mover._movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
        }        
    }
}
