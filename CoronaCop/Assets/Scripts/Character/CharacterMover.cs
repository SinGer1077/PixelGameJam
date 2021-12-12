using System;
using UnityEngine;

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

    private void Start()
    {
        anim=GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontal, 0, vertical);
        _movementDirection.Normalize();

        if (_movementDirection != Vector3.zero)
        {
            if (!borderBrake)
            {
                transform.Translate(_movementDirection * _characterSpeed * Time.deltaTime, Space.World);
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
