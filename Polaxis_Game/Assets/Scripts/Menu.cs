using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Load_Level(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Close_Application()
    {
        Application.Quit();
    }
}
