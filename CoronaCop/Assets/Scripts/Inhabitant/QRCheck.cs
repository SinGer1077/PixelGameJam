using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCheck : MonoBehaviour
{
    [SerializeField]
    private float _timeToCheck;

    [SerializeField]
    private InfectionState _infection;

    private float _timer = 0f;

    private bool _isReadyToCheck = false;

    private void FixedUpdate()
    {
        if (_isReadyToCheck)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _timeToCheck)
        {
            _timer = 0f;

            if (_infection.Infected)
            {
                Debug.Log("Чертила заражён, ломай его");
            }
            else
            {
                Debug.Log("Чертила чист, отпускаем");
            }
        }
    }

    public void BeginCheckQR()
    {
        _isReadyToCheck = true;
    }

    public void StopCheckQR()
    {
        _isReadyToCheck = false;
    }
}
