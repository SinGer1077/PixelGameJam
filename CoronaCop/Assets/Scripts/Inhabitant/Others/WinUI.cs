using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    private LevelCore core;
    void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
    }

    void Update()
    {
        if (core.isWin) setWin();
    }

    public void setWin()
    {
        GetComponent<Canvas>().enabled = true;
        core.running = false;
    }
}
