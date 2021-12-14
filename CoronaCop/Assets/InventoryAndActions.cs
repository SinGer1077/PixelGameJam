using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAndActions: MonoBehaviour
{
    public bool _bandEnough;
    // Start is called before the first frame update
    void Start()
    {
        _bandEnough = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TakeToInventory(GameObject obj)
    {
        if (obj.GetComponent<ItemState>().GetItemName() == "Band")
        {
            if (_bandEnough == false)
            {
                _bandEnough = true;
                Destroy(obj);
            }
            
        }
        
    }

    public bool getBandEnough()
    {
        return _bandEnough;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="ToInventory")
        {
            TakeToInventory(other.gameObject);
        }

        if (other.gameObject.tag == "BarrierBand" && _bandEnough)
        {
            var barrier = other.gameObject.GetComponent<Barrier>();
            barrier.setActive(true);
            _bandEnough = false;
        }
    }
}
