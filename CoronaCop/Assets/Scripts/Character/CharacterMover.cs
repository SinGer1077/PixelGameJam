using System;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private float _characterSpeed;

    public Vector3 _movementDirection;
    private Animator anim;
    private string state="stay";
    private bool borderLand = false;
    [SerializeField] private float speedMultiplier=1.2f;
    [SerializeField] private float speedMultTimer = 2.5f;
    [SerializeField] private int speedMultipleMax = 4;
    public int currentSpeedMultiple;
    private float timer = 0;

    private void Start()
    {
        currentSpeedMultiple = 0;
        anim=GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (currentSpeedMultiple>=1) timer += Time.deltaTime;
        if (timer >= speedMultTimer)
        {
            currentSpeedMultiple = 0;
            timer = 0;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontal, 0, vertical);
        _movementDirection.Normalize();

        if (_movementDirection != Vector3.zero && !borderLand)
        {
            transform.Translate(_movementDirection * _characterSpeed * Time.deltaTime*(Mathf.Pow(speedMultiplier,currentSpeedMultiple)), Space.World);
        }
        else
        {
            if (borderLand)
            {
                transform.Translate(-_movementDirection * _characterSpeed*30 * Time.deltaTime, Space.World);
                borderLand = false;
            } else GetComponent<Rigidbody>().velocity=Vector3.zero;
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
        currentSpeedMultiple++;
        if (currentSpeedMultiple > speedMultipleMax)
        {
            currentSpeedMultiple = speedMultipleMax;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Border")
        {
            borderLand = true;


        }
        else borderLand = false;
    }
}
