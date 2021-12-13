using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndInfectInhabitant : MonoBehaviour
{
    private bool inRecreation = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Inhabitant" && inRecreation == true)
        {
            var state = other.gameObject.transform.GetComponentInChildren<InfectionState>();
            if (!state.Infected )
            {
                state.StartInfection();
            }
        }

        if (other.gameObject.tag == "Recreation")
        {
            inRecreation = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Inhabitant")
        {
            var state = other.gameObject.transform.GetComponentInChildren<InfectionState>();
            if (!state.Infected)
            {
                state.StopInfection();
            }
        }
        if (other.gameObject.tag == "Recreation")
        {
            inRecreation = false;
        }
    }
}
