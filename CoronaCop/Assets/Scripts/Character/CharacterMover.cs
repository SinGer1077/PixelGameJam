using System;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private float _characterSpeed;

    public Vector3 _movementDirection;
    private Animator anim;
    private string state="stay";

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

        transform.Translate(_movementDirection * _characterSpeed * Time.deltaTime, Space.World);
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
}
