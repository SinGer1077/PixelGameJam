using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCheck : MonoBehaviour
{
    [SerializeField]
    private float _timeToCheck;

    [SerializeField]
    private InfectionState _infection;

    [SerializeField]
    private float _timeToUpdateCheck;

    [SerializeField]
    private SpriteRenderer _renderer;

    private float _timer = 0f;

    private float _updateCheckTimer = 0f;

    private bool _isReadyToCheck = false;

    private bool _needToCheck = true;

    public bool NeedToCheck => _needToCheck;

    private void Start()
    {
        SetNeedToCheckTrue();
        _renderer.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_isReadyToCheck && _needToCheck)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _timeToCheck)
        {
            _timer = 0f;
            SetNeedToCheckFalse();

            if (_infection.Infected)
            {
                GetComponentInParent<Car>().CopSayGoOut(); //Гоним его в шею
                Debug.Log("Чертила заражён, ломай его");
            }
            else
            {
                Debug.Log("Чертила чист, отпускаем");
            }
        }

        if (!_needToCheck)
        {
            _updateCheckTimer += Time.deltaTime;

            if (_updateCheckTimer >= _timeToUpdateCheck)
            {
                SetNeedToCheckTrue();
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

    public void SetNeedToCheckTrue()
    {
        _renderer.gameObject.SetActive(true);
        _needToCheck = true;
        _updateCheckTimer = 0f;
        _renderer.color = Color.yellow;
    }  
    
    private void SetNeedToCheckFalse()
    {
        _renderer.gameObject.SetActive(true);
        _needToCheck = false;
        _updateCheckTimer = 0f;
        _renderer.color = Color.green;
    }
}
