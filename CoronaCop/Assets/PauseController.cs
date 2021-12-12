using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    private LevelCore core;
    // Update is called once per frame
    private void Start()
    {
        core = FindObjectOfType<LevelCore>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        core.running = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        core.running = true;
    }
}
