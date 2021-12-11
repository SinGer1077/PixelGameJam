using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recreation : MonoBehaviour
{
    [SerializeField] private float roadToReacreationWidth; //Ширина подходящей к зоне дороги, нужна для корректировки разбега
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false; //Отключение рендера зоны. Изначально включен, чтоб удобно было расставлять
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRoadWidth()
    {
        return roadToReacreationWidth;
    }
}
