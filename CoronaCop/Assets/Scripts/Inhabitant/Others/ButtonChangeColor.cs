using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeColor : MonoBehaviour
{
    private Button button;

    private LevelCore core;
    // Start is called before the first frame update
    void Start()
    {
        core = GameObject.Find("Level").GetComponent<LevelCore>();
        button=GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Click()
    {
        core.changeColor();
    }
}
