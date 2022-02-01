using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Window : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Level_Select()
    {
        Launcher.Level_Select();
    }

    public void Main_Menu()
    {
        Launcher.Main_Menu();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
