using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject background;


    public void Awake()
    {
        background.SetActive(false);
    }

    public void Enable()
    {
        background.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Working");
    }

    public void ExitButton()
    {
        SceneManager.GetSceneByBuildIndex(0);
    }
}
