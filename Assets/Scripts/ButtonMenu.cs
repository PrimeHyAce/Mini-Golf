using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    private void OnEnable()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Menu")
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
