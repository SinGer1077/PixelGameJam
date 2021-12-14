using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    private InventoryAndActions _inventory;

    [SerializeField] private Image _band;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType<InventoryAndActions>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_inventory.getBandEnough())
        {
            _band.enabled = true;
        }
        else
        {
            _band.enabled = false;
        }
    }
}
