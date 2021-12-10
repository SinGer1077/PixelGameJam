using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Color myColor;
    private LevelCore core;
    
    // Start is called before the first frame update
    void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
        setColor(gameObject.GetComponentInParent<UnitedObject>().getColor()); //Отладка
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Car(Clone)")
        {
            if (other.gameObject.GetComponent<MeshRenderer>().material.color == myColor)
            {
                core.colorsPlusCount(myColor);
                other.gameObject.GetComponent<Car>().CarEndPoint();
            }
        }
    }

    public void setColor(Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        myColor = color;
    }
}
