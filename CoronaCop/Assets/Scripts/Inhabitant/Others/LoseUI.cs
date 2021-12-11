using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    private LevelCore core;
    void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (core.isLose) setLose();
    }

    public void setLose()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
