using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private float _characterSpeed;

    [SerializeField]
    private ParticleSystem _runSystem;    

    public Vector3 _movementDirection;
    private Animator anim;
    private string state="stay";
    private bool borderBrake = false;
    [SerializeField] private float speedMultiplier=1.2f;
    [SerializeField] private float speedMultTimer;
    [SerializeField] private int speedMultipleMax;
    public int currentSpeedMultiple;
    private float timer = 0;
    private MultiplierText texter;
    private void Start()
    {
        anim=GetComponentInChildren<Animator>();
        texter = FindObjectOfType<MultiplierText>();
    }

    private void Update()
    {
        if (currentSpeedMultiple>=1) timer += Time.deltaTime;
        if (timer >= speedMultTimer)
        {
            currentSpeedMultiple = 0;
            timer = 0;
            texter.CheckMultiplierToCanvas();
            
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
            

                //anim.enabled = false;
                //anim.SetBool("staying",false);
                anim.SetTrigger("run");
                //anim.enabled = true;
                state = "run";
                //anim.SetBool("running",true);
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
    public void IncreaseMultiplier()
    {
        timer = 0;
        
        if (currentSpeedMultiple >= speedMultipleMax)
        {
            currentSpeedMultiple = speedMultipleMax;
        } else currentSpeedMultiple+=1;

        texter.CheckMultiplierToCanvas();
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
