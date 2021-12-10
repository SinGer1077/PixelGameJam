using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionState : MonoBehaviour
{
    [SerializeField]
    private bool _infected;

    public bool Infected => _infected;
    
}
