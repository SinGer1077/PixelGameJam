using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    [SerializeField]
    private SpriteRenderer _background;

    [SerializeField]
    private Sprite _negativeResultSprite;

    [SerializeField]
    private Sprite _default;

    [SerializeField]
    private Sprite _positiveResultSprite;

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
            _renderer.gameObject.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(25, 25, 1), _timer/_timeToCheck);
        }

        if (_timer >= _timeToCheck)
        {
            _timer = 0f;
            SetNeedToCheckFalse();

            if (_infection.Infected)
            {
                GetComponentInParent<Car>().CopSayGoOut(); //Гоним его в шею
                _renderer.sprite = _positiveResultSprite;
                Debug.Log("Чертила заражён, ломай его");
            }
            else
            {
                _renderer.sprite = _negativeResultSprite;
                Debug.Log("Чертила чист, отпускаем");
            }
            Invoke("ResetToDefaulImage", 0.8f);
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

        _background.gameObject.SetActive(true);
        _renderer.gameObject.SetActive(true);

    }

    public void StopCheckQR()
    {
        if (_needToCheck == true)
        {
            _isReadyToCheck = false;

            _background.gameObject.SetActive(false);
            _renderer.gameObject.SetActive(false);

            _renderer.gameObject.transform.localScale = Vector3.one;
        }
    }

    public void SetNeedToCheckTrue()
    {        
        _needToCheck = true;
        _updateCheckTimer = 0f;
        _renderer.sprite = _default;
        //_renderer.color = Color.yellow;
    }  
    
    private void SetNeedToCheckFalse()
    {        
        _needToCheck = false;
        _updateCheckTimer = 0f;
        //_renderer.color = Color.green;
    }

    private void ResetToDefaulImage()
    {
        _background.gameObject.SetActive(false);
        _renderer.gameObject.SetActive(false);
        _renderer.sprite = _default;
        _renderer.gameObject.transform.localScale = Vector3.one;
    }
}
