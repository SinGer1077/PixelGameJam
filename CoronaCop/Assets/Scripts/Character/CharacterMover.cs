using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _characterSpeed;

    [SerializeField] private ParticleSystem _runSystem;

    public Vector3 _movementDirection;
    private Animator anim;
    private string state = "stay";
    private bool borderBrake = false;
    [SerializeField] private float speedMultiplier = 1.2f;
    [SerializeField] private float speedMultTimer;
    [SerializeField] private int speedMultipleMax;
    private int currentSpeedMultiple = 0;
    private float timer = 0;
    [SerializeField] private GameObject UIBoost;
    private BoostImage imager;
    private float _currentBoostTimer;
    private float[] timing;
    private int currentBoostTimer = 0;

    private void Start()
    {
        timing = new float[6] {0, 12f, 10f, 8f, 6f, 5f};
        imager = UIBoost.GetComponent<BoostImage>();
        anim = GetComponentInChildren<Animator>();
        //imager = FindObjectOfType<BoostImage>();
    }

    private void Update()
    {
        if (currentSpeedMultiple >= 1) timer += Time.deltaTime;
        if (timer >= timing[currentSpeedMultiple] && currentSpeedMultiple != 0) 

{
            currentSpeedMultiple -=1;
            timer = 0;
            imager.CheckMultiplierToCanvas();
            
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontal, 0, vertical);
        _movementDirection.Normalize();

        if (_movementDirection != Vector3.zero)
        {
            if (!borderBrake)
            {
                transform.Translate(_movementDirection * _characterSpeed*Mathf.Pow(speedMultiplier,currentSpeedMultiple) * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(-_movementDirection * _characterSpeed *40f* Time.deltaTime, Space.World);
                borderBrake = false;
            } 
            
            _runSystem.Play();
            _runSystem.gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Rigidbody>().velocity=Vector3.zero;
            StopAnimate();
        }
        if (horizontal != 0f || vertical != 0f)
        {
            anim.SetTrigger("run");
                state = "run";
        } else {
            if (state == "run")
            {                
                //anim.SetBool("running",false);
            }
            anim.SetTrigger("stay"); //anim.SetBool("running", false);
            state = "stay";            
        }
    }
    public int GetMultiplier()
    {
        return currentSpeedMultiple;
    }

    public float GetTimer()
    {
        return timing[currentSpeedMultiple];
    }
    public void IncreaseMultiplier()
    {
        timer = 0;
        
        if (currentSpeedMultiple >= speedMultipleMax)
        {
            currentSpeedMultiple = speedMultipleMax;
        } else currentSpeedMultiple+=1;

        imager.CheckMultiplierToCanvas();
    }
    private void StopAnimate()
    {
        _runSystem.Pause();
        _runSystem.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Border")
        {
            borderBrake = true;
        }
        else
        {
            borderBrake = false;
        }
    }
}
