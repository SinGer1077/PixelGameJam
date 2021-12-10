using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndCheckInhabitant : MonoBehaviour
{      

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InhabitantCheckZone")
        {
            other.gameObject.GetComponent<QRCheck>().BeginCheckQR();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InhabitantCheckZone")
        {
            other.gameObject.GetComponent<QRCheck>().StopCheckQR();
        }
    }
}
