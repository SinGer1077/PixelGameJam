using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierText : MonoBehaviour
{
    private CharacterMover playerMover;
    // Start is called before the first frame update
    void Start()
    {
        playerMover = FindObjectOfType<CharacterMover>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (playerMover.i)
        GetComponent<Text>().text=*/
    }

    public void CheckMultiplierToCanvas()
    {
        var x=playerMover.GetMultiplier();
        gameObject.GetComponentInChildren<Text>().text = "Boost: X" + x;
    }
}
