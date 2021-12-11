using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private GameObject[] __objectsToActivate;    

    public void GameStart()
    {
        Time.timeScale = 1;
        _menu.SetActive(false);

        foreach(GameObject obj in __objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
