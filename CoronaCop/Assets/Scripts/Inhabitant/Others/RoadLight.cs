using UnityEngine;
using UnityEngine.EventSystems;

public class RoadLight : MonoBehaviour, IPointerClickHandler
{
    private int id;
    private bool active = false;

    private LevelCore core;
    private void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
        id = GetComponent<idScript>().getId();
    }

    private void Update()
    {
        active = changeActive();
        changeOpacity(active);
    }

    public void setColor(Color col)
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = col;
    }

    private bool changeActive()
    {
        if (core.statistic[GetComponent<idScript>().getId()].color == core.activeLight)
        {
            return true;
        }
        else return false;
    }

    private void changeOpacity(bool isActive)
    {
        Color stateColor;

            foreach (var element in gameObject.GetComponentsInChildren<Transform>())
            {
                if (element.gameObject.name == "Sphere")
                {
                    stateColor=element.gameObject.GetComponent<MeshRenderer>().material.color;
                    if (isActive)
                    {
                        stateColor.a = 1f;
                    }
                    else
                    {
                        stateColor.a = 0.1f;
                    }
                    
                    
                    element.gameObject.GetComponent<MeshRenderer>().material.color = stateColor;
                    
                }
            }
    }
    public void OnPointerClick (PointerEventData eventData)
    {
        core.changeColor();
    }

}
