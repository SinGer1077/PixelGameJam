using UnityEngine;
using UnityEngine.UI;

public class TimerMoving : MonoBehaviour
{
    [SerializeField]
    private float _timeToEndGame;

    private float _currentTime;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private GameObject _endGamePanel;

    private void Start()
    {
        _slider.value = 1f;
        _currentTime = _timeToEndGame;
    }

    private void Update()
    {
        if (_slider.value > 0)
        {
            _slider.value = (_currentTime - Time.time) / _timeToEndGame;
            if (_slider.value <= 0)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        Debug.Log("Конец");
        Time.timeScale = 0;
        _slider.value = 1f;
        _currentTime = _timeToEndGame;
        _endGamePanel.SetActive(true);
    }
}
