using UnityEngine;

public class RoadLightsInitiator : MonoBehaviour
{
    private LevelCore core;
    private Vector3 startPosition;
    private bool wasinitiate = false;
    void Start()
    {
        startPosition = gameObject.transform.position;
        core = GameObject.Find("Level").GetComponent<LevelCore>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!wasinitiate)
        {
            //Отрисовка светофора
            wasinitiate = true;
            int i = 0;
            foreach (var element in core.statistic)
            {
                //Создать элемент
                GameObject instance = Instantiate(Resources.Load("RoadLight", typeof(GameObject))) as GameObject;
                instance.transform.position = startPosition;
                instance.transform.parent = transform;
                //Назначить цвет
                foreach (var element2 in instance.GetComponentsInChildren<Transform>())
                {
                    if (element2.gameObject.name == "Sphere")
                    {
                        element2.gameObject.GetComponent<MeshRenderer>().material.color = element.color;
                    }
                }
                instance.GetComponent<idScript>().setId(i);
                //Сдвинуть каретку
                startPosition.x +=20;
                i++;
            }

            transform.Rotate(5,130,80);
        }
    }
}
