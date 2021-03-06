using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InfectionState : MonoBehaviour
{
    [SerializeField]
    private bool _infected;

    public bool Infected => _infected;

    [SerializeField]
    private SphereCollider _infectionZone;

    [SerializeField]
    private float _timeToInfect;

    [SerializeField]
    private MeshRenderer _renderer;

    [SerializeField]
    private Material _infectedMaterial;

    [SerializeField]
    private AudioSource _audio;

    private bool _readyToInfect = false;

    private float _timer = 0f;

    private bool inParkState = false;
    //private ParticleSystem em;
    private float timerEm = 4; //Таймер эмиссии частичек чиха

    private ParticleSystem particles;
    //private ParticleSystem.EmissionModule emEn;
    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        particles.GetComponent<Renderer>().enabled = false;
        if (_infected)
        {
            SetInfection();
        }

        //em = GetComponentInChildren<ParticleSystem>();
        //emEn = em.emission;
    }

    public void SetInfection()
    {
        _readyToInfect = false;
        _infected = true;
        gameObject.AddComponent<DetectAndInfectInhabitant>();
        _renderer.material = _infectedMaterial;
       // emEn.enabled = true;
        timerEm = 3;

    }

    public void SetInfectionZoneRadius(float radius)
    {
        _infectionZone.radius = radius;
    }

    private void Update()
    {
        //if (emEn.enabled==true) timerEm -= Time.deltaTime;
        if (timerEm <= 0 )
        {
            //var emEn = em.emission;
            //emEn.enabled = false;
        }
        if (_readyToInfect && !_infected)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeToInfect)
            {
                SetInfection();
                _audio.Play();
                
                particles.GetComponent<Renderer>().enabled = true;
                particles.Play();
                var progressBar = FindObjectOfType<InfectionProgressCounter>();
                if (inParkState)
                {
                    progressBar.IncreaseCountOne();
                }
                
            }
        }
    }

    public void StartInfection()
    {
        _readyToInfect = true;
    }

    public void StopInfection()
    {
        _readyToInfect = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        inParkState = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inParkState = false;
    }
}
