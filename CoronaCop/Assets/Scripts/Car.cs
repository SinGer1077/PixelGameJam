using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;


public class Car : MonoBehaviour
{
    private GameObject enemy;
    private float acceleration;
    private LevelCore levelCore;
    private float maxSpeed;
    private Rigidbody carBody;
    private string manState = "Created"; //Режим движения
    private float timeInZone = 4f;
    private float spreadingRadius;
    private Vector3 endPoint;
    private Quaternion rotTarget;
    private Rigidbody rb;
    private float timeToRunningOut = 3f; //Время исчезновения
    private float timerRunOut = 0;
    private float aTimer = 0;
    private bool arrivedToRecreation = false;
    
    
    void Start()
    {
        levelCore = GameObject.Find("Level").GetComponent<LevelCore>();
        rb = GetComponent<Rigidbody>();
        carBody = gameObject.GetComponent<Rigidbody>();
        enemy = FindEnemy(); //Поиск направления движения
        if (enemy!=null) {
            
            var direction = enemy.transform.position - gameObject.transform.position;
            direction.y = 0;
            transform.rotation=Quaternion.LookRotation (enemy.transform.position - gameObject.transform.position, Vector3.up);
            rotTarget = transform.rotation;
            gameObject.GetComponent<Renderer>().enabled = true;
            
        }

    }
    
    void FixedUpdate()
    {
        if (levelCore == null) return;
        if (levelCore.getRunning())
        {
            if (manState == "Created")
            {
                spreadingRadius = enemy.GetComponent<Recreation>().GetRoadWidth();
                endPoint = enemy.transform.position +
                           new Vector3(Random.Range(-spreadingRadius, spreadingRadius), 0, Random.Range(-spreadingRadius,
                               spreadingRadius));
                manState = "toEnemy";
            }
            if (manState == "toEnemy")
            {
                ToPoint();
                transform.rotation=Quaternion.Lerp(transform.rotation, rotTarget, 0.08f);
                //Проверка на заход в зону - при коллизии с триггером.
            }

            if (manState == "toPoint")
            {
                transform.rotation=Quaternion.Lerp(transform.rotation, rotTarget, 0.08f);
                ToPoint();
            }

            if (manState == "toPoint" && (endPoint - transform.position).magnitude < 5f)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                manState = "waiting";
            }

            if (manState == "waiting")
            {
                BrakingAfterCollision();
                timeInZone -= Time.deltaTime;
                if (timeInZone <= 0)
                {
                    manState = "runningOut";
                }
            }

            if (manState == "runningOut")
            {

                if (timerRunOut == 0)
                {
                    endPoint = FindSpawn().transform.position;
                    rotTarget=Quaternion.LookRotation (endPoint - gameObject.transform.position, Vector3.up);
                    GetComponent<BoxCollider>().enabled = false;
                    rb.constraints = RigidbodyConstraints.FreezePositionY;
                    aTimer = timeToRunningOut;
                    maxSpeed*= 2f;
                    StartCoroutine(fadeInAndOut(gameObject, false, timeToRunningOut));
                }

                timerRunOut += Time.deltaTime;
                transform.rotation=Quaternion.Lerp(transform.rotation, rotTarget, 0.08f);
                ToPoint();
                if (timerRunOut >= timeToRunningOut)
                {
                    manState = "Deleting";}
            }

        }
    }

    public void CopSayGoOut()
    {
        manState = "runningOut";
        timerRunOut = 0;
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Recreation" && !arrivedToRecreation)
        {
            arrivedToRecreation = true;
            var vel = GetComponent<Rigidbody>().velocity.magnitude;
            manState = "toPoint";
            spreadingRadius = other.transform.lossyScale.x/2f;
            endPoint =other.transform.position + new Vector3(Random.Range(-spreadingRadius,spreadingRadius),0,Random.Range(-spreadingRadius,spreadingRadius));
            GetComponent<Rigidbody>().velocity = (endPoint - transform.position).normalized * vel;
            rotTarget=Quaternion.LookRotation (endPoint - gameObject.transform.position, Vector3.up);
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
        if (manState == "toEnemy")
        {
            Vector3 vector = Vector3.Normalize(endPoint - gameObject.transform.position);
            gameObject.GetComponent<Rigidbody>().AddForce(vector*acceleration);    
        }

        if (manState == "toPoint" || manState=="runningOut")
        {
            Vector3 vector = Vector3.Normalize(endPoint - gameObject.transform.position);
            gameObject.GetComponent<Rigidbody>().AddForce(vector*acceleration);    
        }

    }

    private void CarBraking(int x)
    {
        Vector3 vector = Vector3.Normalize(enemy.transform.position - gameObject.transform.position);
        gameObject.GetComponent<Rigidbody>().AddForce(-x*carBody.velocity.normalized*acceleration);    
    }

    public void SetMaxSpeed(float x)
    {
        maxSpeed = x;
    }

    private GameObject FindEnemy()
    {
        var points = GameObject.FindGameObjectsWithTag("Recreation");
        float minMagnitude = 0f;
        GameObject target = null;
        float currentDistance;
        foreach (var point in points)
        {
            currentDistance = (transform.position - point.transform.position).magnitude;
            if (target==null)
            {
                minMagnitude = currentDistance;
                target = point;
            }

            if (minMagnitude > currentDistance)
            {
                target = point;
                minMagnitude = currentDistance;
            }
            
        }
        Debug.Log("Рекреация найдена"+target);
        return target;
    }

    private GameObject FindSpawn()
    {
        var points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        return points.First(point =>
            point.GetComponent<idScript>().getId() == gameObject.GetComponent<idScript>().getId());
    }

    private void ToPoint()
    {
        if (carBody.velocity.magnitude < maxSpeed)
        {
            CarMoving();
                
        }
    }

    private void BrakingAfterCollision()
    {
        if (rb.velocity.magnitude > 10 && rb.velocity.magnitude != 0f)
        {
            rb.velocity /= 1.5f;
                    
        }
        if (rb.velocity.magnitude < 10 && rb.velocity.magnitude != 0)
        {
            rb.velocity = Vector3.zero;
        } 
    }

IEnumerator fadeInAndOut(GameObject objectToFade, bool fadeIn, float duration)
{
    float counter = 0f;

    //Set Values depending on if fadeIn or fadeOut
    float a, b;
    if (fadeIn)
    {
        a = 0;
        b = 1;
    }
    else
    {
        a = 1;
        b = 0;
    }

    int mode = 0;
    Color currentColor = Color.clear;

    SpriteRenderer tempSPRenderer = objectToFade.GetComponent<SpriteRenderer>();
    Image tempImage = objectToFade.GetComponent<Image>();
    RawImage tempRawImage = objectToFade.GetComponent<RawImage>();
    MeshRenderer tempRenderer = objectToFade.GetComponent<MeshRenderer>();
    Text tempText = objectToFade.GetComponent<Text>();

    //Check if this is a Sprite
    if (tempSPRenderer != null)
    {
        currentColor = tempSPRenderer.color;
        mode = 0;
    }
    //Check if Image
    else if (tempImage != null)
    {
        currentColor = tempImage.color;
        mode = 1;
    }
    //Check if RawImage
    else if (tempRawImage != null)
    {
        currentColor = tempRawImage.color;
        mode = 2;
    }
    //Check if Text 
    else if (tempText != null)
    {
        currentColor = tempText.color;
        mode = 3;
    }

    //Check if 3D Object
    else if (tempRenderer != null)
    {
        currentColor = tempRenderer.material.color;
        mode = 4;

        //ENABLE FADE Mode on the material if not done already
        tempRenderer.material.SetFloat("_Mode", 2);
        tempRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        tempRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        tempRenderer.material.SetInt("_ZWrite", 0);
        tempRenderer.material.DisableKeyword("_ALPHATEST_ON");
        tempRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        tempRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        tempRenderer.material.renderQueue = 3000;
    }
    else
    {
        yield break;
    }

    while (counter < duration)
    {
        counter += Time.deltaTime;
        float alpha = Mathf.Lerp(a, b, counter / duration);

        switch (mode)
        {
            case 0:
                tempSPRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                break;
            case 1:
                tempImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                break;
            case 2:
                tempRawImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                break;
            case 3:
                tempText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                break;
            case 4:
                tempRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                break;
        }
        yield return null;
    }
}
}