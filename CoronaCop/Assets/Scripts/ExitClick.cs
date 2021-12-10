using UnityEngine;
using UnityEngine.UI;

public class ExitClick : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Click()
    {
        Application.Quit();
    }
}
