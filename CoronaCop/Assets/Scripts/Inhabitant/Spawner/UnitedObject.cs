using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitedObject : MonoBehaviour
{
    public Color color;
    public int id;

    private LevelCore core;
    // Start is called before the first frame update
    void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
        List<GameObject> parts=new List<GameObject>();
        foreach (var element in GetComponentsInChildren<Transform>()) {
            parts.Add(element.gameObject);
        }
        core.statistic.Add(new LevelCore.Colors()
        {
            color=color,
            countWin = 0,
            endPoint = parts.First(part =>part.name=="EndCube"),
            spawn = parts.First(part =>part.name=="Spawn"),

        });
        for (int i = 0; i < core.statistic.Count; i++)
        {
            if (color == core.statistic[i].color)
            {
                id = i;
            }
        }
        foreach (var element in GetComponentsInChildren<Transform>()) //присвоение id из главного списка каждому компоненту трассы, для удобного поиска
        {
            if (element.GetComponent<idScript>())
            {
                element.GetComponent<idScript>().setId(id);
            }
            
        }
        core.firstChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color getColor()
    {
        return color;
    }
}
