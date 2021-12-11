using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoverToCharacter : MonoBehaviour
{
    [SerializeField]
    private Transform _character;

    [SerializeField]
    private Vector3 _cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        CameraMove();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        Camera.main.transform.position = new Vector3(_character.position.x + _cameraPosition.x,
            _character.position.y + _cameraPosition.y,
            _character.position.z + _cameraPosition.z);
    }
}
