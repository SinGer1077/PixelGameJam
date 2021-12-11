using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndInfectInhabitant : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Inhabitant")
        {
            var state = other.gameObject.transform.GetComponentInChildren<InfectionState>();
            if (!state.Infected)
            {
                state.StartInfection();
            }
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
    }
}
