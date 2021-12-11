using UnityEngine;
using UnityEngine.UI;

public class InfectionProgressCounter : MonoBehaviour
{
    [SerializeField]
    private RectTransform _imageTransform;

    [SerializeField]
    private Text _infectionProgress;

    [SerializeField]
    private float _infectionMax;

    [SerializeField]
    private RectTransform _parentWidth;

    [SerializeField]
    private ParticleSystem _system;

    private int _currentCount = 0;

    private float _transformStep;

    private RectTransform _defaultTransform;

    private float timer;

    private void Start()
    {
        _defaultTransform = _imageTransform;
        _transformStep = (_parentWidth.rect.width - _imageTransform.anchoredPosition.x) / _infectionMax;

        ResetToDefault();       
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            IncreaseCount();
            timer = 0;
        }
    }

    public void SetInfectionMax(int value)
    {
        _infectionMax = value;
    }

    public void IncreaseCount()
    {
        _currentCount++;
        _system.Play();
        MoveProgressBar();
        CheckGameLosed();
        Invoke("StopParticleSystem", 1f);
    }

    private void StopParticleSystem()
    {
        _system.Stop();
    }

    private void CheckGameLosed()
    {
        if (_currentCount >= _infectionMax)
        {

        }
    }

    private void MoveProgressBar()
    {
        _imageTransform.anchoredPosition = new Vector2(_imageTransform.anchoredPosition.x + _transformStep, _imageTransform.anchoredPosition.y);
        _infectionProgress.text = _currentCount.ToString() + "/" + _infectionMax.ToString();
    }

    private void ResetToDefault()
    {
        _infectionProgress.text = _currentCount.ToString()+ "/" + _infectionMax.ToString();
        _imageTransform = _defaultTransform;        
    }
}
