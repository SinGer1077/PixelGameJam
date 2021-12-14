using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float timeToDie = 25; //Время до исчезновения
    private float timer = 0;
    private Renderer _rend;
    private Collider _collider;
    private bool _isActive;
    [SerializeField] private GameObject band;
    private Collider _activationCollider;
    public bool startState;
    public float startDelay;

    
    // Start is called before the first frame update
    void Start()
    {
        _rend = band.GetComponent<Renderer>();
        _collider = band.GetComponent<Collider>();
        _activationCollider = GetComponent<Collider>();
        setActive(startState);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer<=0) setActive(false);
        }
    }

    public void SetTimeToDie(float x)
    {
        timeToDie = x;
        
    }
    public void setActive(bool x)
    {
        if (x)
        {
           
            timer = timeToDie;
            if (startState)
                timer += startDelay;
            _rend.enabled = true;
            _collider.enabled = true;
            _isActive = true;
            _activationCollider.enabled = false;
        }
        else
        {
            timer = 0;
            _rend.enabled = false;
            _collider.enabled = false;
            _isActive = false;;
            _activationCollider.enabled = true;
        }

    }
}
