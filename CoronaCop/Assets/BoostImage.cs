using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostImage : MonoBehaviour
{
    private CharacterMover playerMover;
    private LevelCore core;
    private float timer1=1;
    [SerializeField] private Sprite level0;
    [SerializeField] private Sprite level1;
    [SerializeField] private Sprite level2;
    [SerializeField] private Sprite level3;
    [SerializeField] private Sprite level4;
    [SerializeField] private Sprite level5;
    private Text texter;
    private ParticleSystem partirces;
    private int laststate = 0;
    private float progressMaxTimer=0;
    private float progressTimer=0;
    [SerializeField] private Image progressBar;
    private Vector3 scrollTrasform;
    private Image imager;
    // Start is called before the first frame update
    void Start()
    {
        scrollTrasform = progressBar.transform.localScale;
        scrollTrasform.x = 0;
        progressBar.transform.localScale = scrollTrasform;
        partirces = GetComponent<ParticleSystem>();
        texter = gameObject.GetComponentInChildren<Text>();
        imager = GetComponent<Image>();
        playerMover = FindObjectOfType<CharacterMover>();
        core = FindObjectOfType<LevelCore>();
        imager.sprite = level0;
        
    }
    
    void Update()
    {
        if (core.running)
        {
            if (imager.enabled == false) imager.enabled = true;
        } else if (imager.enabled == true) imager.enabled = false;

        if (progressMaxTimer > 0)
        {
            progressTimer -= Time.deltaTime;
            
            scrollTrasform.x = 0.95f * (progressTimer / progressMaxTimer);
            progressBar.transform.localScale = scrollTrasform;
        }

    }

    public void CheckMultiplierToCanvas()
    {
        var x=playerMover.GetMultiplier();
        var t=playerMover.GetTimer();

        if (x == 0)
        {
            if (laststate != 0)
            {
                imager.sprite = level0;
                laststate = x;
                progressMaxTimer = t;
                progressTimer = progressMaxTimer;
            }
        }

        if (x == 1)
        {
            if (laststate < 1)
            {
                partirces.Play();
            }
            imager.sprite = level1;
            laststate = x;
            progressMaxTimer = t;
            progressTimer = progressMaxTimer;
            
        }
        if (x == 2)
        {
            if (laststate < 2)
            {
                partirces.Play();
            }

            laststate = x;
            imager.sprite = level2;
            progressMaxTimer = t;
            progressTimer = progressMaxTimer;
            
        }
        if (x == 3)
        {
            imager.sprite = level3;
            if (laststate < 3)
            {
                partirces.Play();
            }
            laststate = x;
            progressMaxTimer = t;
            progressTimer = progressMaxTimer;
        }
        if (x == 4)
        {
            imager.sprite = level4;
            if (laststate < 4)
            {
                partirces.Play();
            }
            laststate = x;
            progressMaxTimer = t;
            progressTimer = progressMaxTimer;
            
        }
        if (x == 5)
        {
            imager.sprite = level5;
            if (laststate < 5)
            {
                partirces.Play();
            }
            laststate = x;
            progressMaxTimer = t;
            progressTimer = progressMaxTimer;
        }
    }
}
