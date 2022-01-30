using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Level_Data Level_Data;
    public TMP_Text timer;

    private void Awake()
    {
        instance = this;
        Level_Data.isComplete = false;
    }

    public void Attempt_Completion()
    {
        StartCoroutine(Completion_Clock());
        foreach (MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>())
        {
            mono.TryGetComponent(out IControllable c);
            if (c != null) { c.Disable_Controls(); }
        }
    }

    public IEnumerator Completion_Clock()
    {
        float time = 0;
        while(time < Level_Data.max_time)
        {
            time += Time.deltaTime;
            timer.text = "Time: " + time.ToString("000.000");
            yield return new WaitForEndOfFrame();
            if(Level_Data.isComplete == true)
            {
                Level_Data.Completion_Time = time;
                break;
            }
        }
        if (!Level_Data.isComplete)
        {
            timer.text = "Time: 000.000";
            Reset_Level();
        }
    }
    public void Complete_Level()
    {
        Level_Data.isComplete = true;
    }

    public void Next_Level()
    {

    }

    public void Reset_Level()
    {
        foreach(MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>())
        {
            mono.TryGetComponent(out IReset r);
            if(r != null) { r.Reset(); }
            mono.TryGetComponent(out IControllable c);
            if (c != null) { c.Enable_Controls(); }
        }
    }

    public void Pause_Game()
    {
        Time.timeScale = 0;
    }

    public void Resume_Game()
    {
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        Level_Data.Magnet_Count = 0;
    }
}
