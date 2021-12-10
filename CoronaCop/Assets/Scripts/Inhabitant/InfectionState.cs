using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool _readyToInfect = false;

    private float _timer = 0f;

    private void Start()
    {
        if (_infected)
        {
            SetInfection();
        }
    }

    public void SetInfection()
    {
        _readyToInfect = false;
        _infected = true;
        gameObject.AddComponent<DetectAndInfectInhabitant>();
        _renderer.material = _infectedMaterial;
    }

    public void SetInfectionZoneRadius(float radius)
    {
        _infectionZone.radius = radius;
    }

    private void Update()
    {
        if (_readyToInfect && !_infected)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeToInfect)
            {
                SetInfection();
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
}
