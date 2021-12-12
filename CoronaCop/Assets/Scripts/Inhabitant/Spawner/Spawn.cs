using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 2f; //Частота проверки спавна
    private float acceleration; //Ускорение машинки
    private float maxSpeed; 
    [FormerlySerializedAs("accelerationRandomization")] public bool speedRandomization = false; //Параметр, включающий рандомный элемент в скорости машинки
    [SerializeField] private float infectedChance;
    [SerializeField] private float roadWidth;
    [SerializeField] private float speedVillagerToZone; //Предел максимальной скорости машинки
    [SerializeField] private float speedVillagerFromZone;
    [SerializeField] private float chanceToSpawn;
    [SerializeField] private int numToChangeChance = 10;
    [SerializeField] private float decreaseChance = 0.05f;
    private float currentDecreaseChance = 0f;

    private Color carColor; //Не удалять пока
    private float spawnTime;
    private LevelCore levelCore;
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        maxSpeed = speedVillagerToZone;
        acceleration = maxSpeed+15f;
        setColor(gameObject.GetComponentInParent<UnitedObject>().getColor());
        levelCore=GameObject.Find("Level").GetComponent<LevelCore>();
        spawnTime =0;
    }

    // Update is called once per frame
    void Update()
    {
        //Таймер спавна
        if (levelCore.running)
        {
            if (spawnTime <= 0f)
            {
                if (levelCore.getTotalVisitors() >= numToChangeChance)
                {
                    currentDecreaseChance = decreaseChance;
                    if (decreaseChance > chanceToSpawn) currentDecreaseChance = chanceToSpawn;
                }
                else currentDecreaseChance = 0f;
                if (Random.Range(0, 1f) > (1f - chanceToSpawn+currentDecreaseChance))
                {
                    createCar();
                    
                }
                spawnTime = timeToSpawn;

            }

            if (spawnTime > 0) spawnTime -= Time.deltaTime;
        }
    }

    
    
    private void createCar()
    {
        GameObject instance = Instantiate(Resources.Load("Visitor", typeof(GameObject))) as GameObject;
        var Car = instance.GetComponent<Car>();
        instance.GetComponent<idScript>().setId(GetComponent<idScript>().getId());
        instance.GetComponent<MeshRenderer>().material.color = Color.yellow;
        instance.transform.position = transform.position;
        instance.transform.position = new Vector3(instance.transform.position.x+Random.Range(-roadWidth,roadWidth),instance.transform.position.y,instance.transform.position.z+Random.Range(-roadWidth,roadWidth));
        instance.transform.parent = transform.parent;
        if ((1f - Random.Range(0f, 1f)) < infectedChance)
        {
            instance.GetComponentInChildren<InfectionState>().SetInfection();
        }
        var speed = acceleration;
        //if (accelerationRandomization) speed += Random.Range(-acceleration / 4, acceleration / 4);
        instance.GetComponent<Car>().setSpeed(speed);
        Car.SetSpeedVillagerFromZone(speedVillagerFromZone);
        if (speedRandomization)
        {
            instance.GetComponent<Car>().SetMaxSpeed(maxSpeed + Random.Range(-maxSpeed * 0.15f,maxSpeed*0.15f));
        }
        else
        {
            instance.GetComponent<Car>().SetMaxSpeed(maxSpeed);
        }
        
        instance.GetComponent<idScript>().setId(gameObject.GetComponent<idScript>().getId());
    }

    void setColor(Color color)
    {
        carColor = color;
    }
}
