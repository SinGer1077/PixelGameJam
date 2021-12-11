using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LevelCore : MonoBehaviour
{
    public Color activeLight;

    public List<Colors> statistic= new List<Colors>();
    public bool running = true; //вклчючение/заморозка геймплея
    public bool isLose = false;
    public bool isWin = false;
    public int winRate; //Количество машинок для выигрыша
    private float timeToScanInhabitant = 1f;
    private float timerToScan;

    private void Start()
    {
        timerToScan = timeToScanInhabitant;
    }

    void Update()
    {
        //Проверка на выигрыщ
        timerToScan -= Time.deltaTime;
        if (timerToScan <= 0)
        {
            timerToScan = timeToScanInhabitant;
            
        }
        var state1 = true;
        var i = 1;
        foreach (var element in statistic)
        {
            if (element.countWin < winRate)
            {
                state1 = false;
            }
        }

        if (state1)
        {
            isWin = true;
        }
    }
    public class Colors    //Класс данных о спавнах, цветах машин и т.д. со ссылками на обьекты
    
    {
        public Color color { get; set; }
        public int countWin { get; set; }
        public GameObject spawn { get; set; }
        public GameObject progress { get; set; }
        public GameObject endPoint { get; set; }

    }    
    public Color getActiveLight()
    {
        return activeLight;
    }

    public void colorsPlusCount(Color color)
    {
        foreach (var element in statistic)
        {
            if (element.color == color)
            {
                element.countWin++;
            }
        }
    }

    public void firstChangeColor()
    {
        activeLight = statistic[0].color;
    }
    public void changeColor()
    {
        for (int i = 0; i < statistic.Count; i++)
        {
            if (statistic[i].color == activeLight)
            {
                
                if (i + 1 < statistic.Count)
                {
                    activeLight = statistic[i + 1].color;
                    break;
                }
                else
                {
                    activeLight = statistic[0].color;
                    break;
                }
            }
        }
    }
    public bool getRunning()
    {
        return running;
    }

    private void setRunning(bool state)
    {
        running = state;
    }

    public void gameOver()
    {
        setRunning(false);
        setLose(true);
    }

    public void setLose(bool x)
    {
        isLose = x;
    }
}
