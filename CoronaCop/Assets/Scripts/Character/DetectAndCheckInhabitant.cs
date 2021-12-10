using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndCheckInhabitant : MonoBehaviour
{      

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InhabitantCheckZone")
        {
            var state = other.gameObject.GetComponent<QRCheck>();
            if (state.NeedToCheck)
            {
                state.BeginCheckQR();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InhabitantCheckZone")
        {
            var state = other.gameObject.GetComponent<QRCheck>();
            state.StopCheckQR();
        }
    }
}
