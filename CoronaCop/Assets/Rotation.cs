using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 rotation = new Vector3(0, 0, 1);
    void FixedUpdate()
    {
        transform.Rotate(rotation * 4 * Time.deltaTime);
    }
}
