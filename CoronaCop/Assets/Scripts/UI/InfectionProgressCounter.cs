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
    private ParticleSystem _plusOne;

    [SerializeField]
    private ParticleSystem _plusTwo;

    [SerializeField]
    private GameObject _endGamePanel;

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
        //timer += Time.deltaTime;
        //if (timer > 2)
        //{
        //    IncreaseCountTwo();
        //    timer = 0;
        //}


    }

    public void SetInfectionMax(int value)
    {
        _infectionMax = value;
    }

    public void IncreaseCountOne()
    {
        _currentCount++;
        _plusOne.Play();
        MoveProgressBar(1);
        CheckGameLosed();
        Invoke("StopParticleSystemFirst", 1f);
    }

    public void IncreaseCountTwo()
    {
        _currentCount++;
        _currentCount++;
        _plusTwo.Play();
        MoveProgressBar(2);
        CheckGameLosed();
        Invoke("StopParticleSystemSecond", 1f);
    }

    private void StopParticleSystemFirst()
    {
        _plusOne.Stop();
    }
    private void StopParticleSystemSecond()
    {
        _plusTwo.Stop();
    }

    private void CheckGameLosed()
    {
        if (_currentCount >= _infectionMax)
        {
            _currentCount = 0;
            Time.timeScale = 0;
            _endGamePanel.SetActive(true);
        }
    }

    private void MoveProgressBar(int count)
    {
        _imageTransform.anchoredPosition = new Vector2(_imageTransform.anchoredPosition.x + (_transformStep * count), _imageTransform.anchoredPosition.y);
        _infectionProgress.text = _currentCount.ToString() + "/" + _infectionMax.ToString();
    }

    private void ResetToDefault()
    {
        _infectionProgress.text = _currentCount.ToString()+ "/" + _infectionMax.ToString();
        _imageTransform = _defaultTransform;

        _plusOne.Stop();
        _plusTwo.Stop();
    }
}
