using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Level_Data Level_Data;
    public TMP_Text timer;
    private Controls controls;
    public GameObject Completion_Window;

    private void Awake()
    {
        instance = this;
        controls = controls == null ? new Controls() : controls;
        controls.Player.Reset.performed += Request_Reset;
        controls.Player.Reset.Enable();
        Level_Data.isComplete = false;
    }

    private void Request_Reset(InputAction.CallbackContext context)
    {
        if (Level_Data.isComplete == false)
        {
            Reset_Level();
        }
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
            if (Level_Data.isComplete)
            {
                Level_Data.Completion_Time = time;
                Completion_Window.SetActive(true);
                break;
            }
            time += Time.deltaTime;
            timer.text = time.ToString("0.000s");
            yield return new WaitForEndOfFrame();          
        }
        if (!Level_Data.isComplete)
        {
            timer.text = "0.000s";
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
        StopAllCoroutines();
        timer.text = "0.000s";
        foreach (MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>())
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
        controls.Player.Reset.Disable();
        Level_Data.Magnet_Count = 0;
    }
}
