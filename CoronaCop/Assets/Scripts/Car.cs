using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Car : MonoBehaviour
{
    private GameObject enemy;

    private float acceleration=200;

    private Color carColor;
    private LevelCore levelCore;
    private float maxSpeed;
    private Rigidbody carBody;
    
    // Start is called before the first frame update
    void Start()
    {
        levelCore = GameObject.Find("Level").GetComponent<LevelCore>();
        carColor = gameObject.GetComponent<MeshRenderer>().material.color;
        carBody = gameObject.GetComponent<Rigidbody>();
        enemy = FindEnemy(); //Поиск направления движения
        if (enemy!=null) {var direction = enemy.transform.position - gameObject.transform.position;
            direction.y = 0;
            transform.rotation=Quaternion.LookRotation (enemy.transform.position - gameObject.transform.position, Vector3.up);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            
        }

    }
    
    void FixedUpdate()
    {
        if (levelCore == null) return;
        if (levelCore.getActiveLight() == carColor && levelCore.getRunning())
        {
            if (carBody.velocity.magnitude < maxSpeed)
            {
                CarMoving();
                
            }

        }
        else
        {

            if (carBody.velocity.magnitude > 25)
            {
               
                CarBraking(4);
                
            }
            if (carBody.velocity.magnitude < 25 && carBody.velocity.magnitude>0)
                    
            {
                carBody.AddForce(-carBody.velocity,ForceMode.VelocityChange);
            } 
        }



    
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Car(Clone)")
        {
            levelCore.gameOver();
        }
    }

    public void setSpeed(float x)
    {
        acceleration = x;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }

    private void CarMoving()
    {
        Vector3 vector = Vector3.Normalize(enemy.transform.position - gameObject.transform.position);
        gameObject.GetComponent<Rigidbody>().AddForce(vector*acceleration);    
    }

    private void CarBraking(int x)
    {
        Vector3 vector = Vector3.Normalize(enemy.transform.position - gameObject.transform.position);
        gameObject.GetComponent<Rigidbody>().AddForce(-x*carBody.velocity.normalized*acceleration);    
    }

    public void CarEndPoint()
    {
        Destroy(gameObject);
    }

    public void SetMaxSpeed(float x)
    {
        maxSpeed = x;
    }

    private GameObject FindEnemy()
    {
        var points = GameObject.FindGameObjectsWithTag("EndPoint");
        return points.First(point =>
            point.GetComponent<idScript>().getId() == gameObject.GetComponent<idScript>().getId());
       // return points.FirstOrDefault(point => point.GetComponent<MeshRenderer>().material.color == carColor);
    }
}
