using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierText : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        texter = gameObject.GetComponentInChildren<Text>();
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
        texter.text = "BOOST: X" + x;
        if (x == 0)
        {
            texter.color=Color.black;
        }

        if (x == 1)
        {
            texter.color=Color.white;
        }
        if (x == 2)
        {
            texter.color=Color.yellow;
        }
        if (x >= 3)
        {
            texter.color=Color.red;
        }
    }
}
