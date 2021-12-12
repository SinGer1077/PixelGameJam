using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierText : MonoBehaviour
{
    private CharacterMover playerMover;
    private LevelCore core;
    private float timer1=1;
    // Start is called before the first frame update
    void Start()
    {
        playerMover = FindObjectOfType<CharacterMover>();
        core = FindObjectOfType<LevelCore>();
    }

    // Update is called once per frame
    void Update()
    {
        timer1+=Time.deltaTime;
        if (timer1 > 1f)
        {
            timer1 = 0;
            if (core.running)
            {
                GetComponent<Canvas>().enabled = true;
            } else GetComponent<Canvas>().enabled = false;
        }
        /*if (playerMover.i)
        GetComponent<Text>().text=*/
    }

    public void CheckMultiplierToCanvas()
    {
        var x=playerMover.GetMultiplier();
        gameObject.GetComponentInChildren<Text>().text = "Boost: X" + x;
    }
}
