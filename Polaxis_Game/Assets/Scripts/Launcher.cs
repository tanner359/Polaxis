using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Launcher
{
    public static void Exit_Application()
    {
        Application.Quit();
    }

    public static void Load_Level(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void Level_Select()
    {
        SceneManager.LoadScene("Level_Select");
    }

    public static void Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
