using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private Vector2 _gameResolution;

    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private GameObject[] __objectsToActivate;

    private LevelCore core;

    private void Awake()
    {
        Screen.SetResolution((int)_gameResolution.x, (int)_gameResolution.y, false);
        
    }

    private void Start()
    {
        core = FindObjectOfType<LevelCore>();
        core.running = false;   
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        _menu.SetActive(false);
        core.running = true;
        foreach(GameObject obj in __objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
