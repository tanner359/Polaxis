using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause_Window : Polaxis_UI
{
    private new void OnEnable()
    {
        base.OnEnable();
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
