using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawn : MonoBehaviour
{
    public Color carColor=Color.green;
    public float spawnSpeed = 2; //Частота спавна машинок
    private float acceleration = 150; //Ускорение машинки
    public float maxSpeed = 300; //Предел максимальной скорости машинки
    public bool accelerationRandomization = false; //Параметр, включающий рандомный элемент в скорости машинки
    public bool spawnRandomization = false; //Параметр, включающий рандомный элемент в промежутках спавна машинок

    private float spawnTime;
    public LevelCore levelCore;
    void Start()
    {
        setColor(gameObject.GetComponentInParent<UnitedObject>().getColor());
        levelCore=GameObject.Find("Level").GetComponent<LevelCore>();
        spawnTime =0;
    }

    // Update is called once per frame
    void Update()
    {
        //Таймер спавна
        if (levelCore.getActiveLight() == carColor && levelCore.running)
        {
            if (spawnTime <= 0)
            {
                
                createCar();
                spawnTime = spawnSpeed;
                
                if (spawnRandomization) {spawnTime+= Random.Range(0f, spawnSpeed / 2f);}
                
            }

            if (spawnTime > 0) spawnTime -= Time.deltaTime;
        }
    }

    
    
    private void createCar()
    {
        GameObject instance = Instantiate(Resources.Load("Car", typeof(GameObject))) as GameObject;
        instance.GetComponent<idScript>().setId(GetComponent<idScript>().getId());
        instance.GetComponent<MeshRenderer>().material.color = carColor;
        instance.transform.position = transform.position;
        instance.transform.position = new Vector3(instance.transform.position.x,7.5f,instance.transform.position.z);
        instance.transform.parent = transform.parent;
        var speed = acceleration;
        if (accelerationRandomization) speed += Random.Range(-acceleration / 4, acceleration / 4);
        instance.GetComponent<Car>().setSpeed(speed);
        instance.GetComponent<Car>().SetMaxSpeed(maxSpeed);
        instance.GetComponent<idScript>().setId(gameObject.GetComponent<idScript>().getId());
    }

    void setColor(Color color)
    {
        carColor = color;
    }
}
