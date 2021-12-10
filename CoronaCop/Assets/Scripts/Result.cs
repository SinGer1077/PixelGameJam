using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private LevelCore core;
    private int winRate;
    private bool initialized = false;
    void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
        winRate = core.winRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            initialized = true;
            Vector3 startpos = new Vector3(-60, -130, 0);
            for (int i = 0; i < core.statistic.Count; i++)
            {
                GameObject instance = Instantiate(Resources.Load("ProgressBar", typeof(GameObject)), GetComponent<Canvas>().transform, true) as GameObject;
                instance.GetComponent<idScript>().setId(i);
                instance.transform.position = startpos;
                instance.GetComponent<Text>().transform.localPosition = GetComponent<Canvas>().transform.position+startpos;
                instance.GetComponent<Text>().color = core.statistic[i].color;
                
                
                startpos.y -= 20;
                core.statistic[i].progress = instance;
            }
        }
        
        foreach (var element in core.statistic)
        {
            String text="";
            for (int i = 1; i <= winRate; i++)
            {
                if (i <= element.countWin)
                {
                    text += "=";
                }
                else
                {
                    text += "_";
                }
            }
            element.progress.GetComponent<Text>().text = text;
        }

        
    }
}
