using UnityEngine;
using Random = UnityEngine.Random;

public class PredmetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject predmet;

    [SerializeField] private float timer;

    [SerializeField] private float timerRandom;

    [SerializeField] private float placeRandomization;
    [SerializeField] private bool startState;
    private GameObject _controlObj=null;

    private float _currentTimer;
    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = timer+Random.Range(0,timerRandom);
        if (startState) {Placing(predmet);}
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTimer > 0 && _controlObj == null)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= 0)
            {
                Placing(predmet);
                _currentTimer=timer+Random.Range(0,timerRandom);
            }
        }
    }

    private void Placing(GameObject obj)
    {
        GameObject newObj = Instantiate(obj) as GameObject;
        newObj.transform.parent = this.transform;
        newObj.transform.position=this.transform.position + new Vector3(Random.Range(-placeRandomization, placeRandomization), 0,
            Random.Range(-placeRandomization, placeRandomization));
        newObj.tag = "ToInventory";
        _controlObj = newObj;

    } 
}
