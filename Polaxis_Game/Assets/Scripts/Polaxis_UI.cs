using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Polaxis_UI : MonoBehaviour
{
    [Header("Base UI")]
    public GameObject OnActiveSelection;

    private EventSystem _eventSystem;

    public void Awake()
    {
        _eventSystem = EventSystem.current;
        if (_eventSystem == null)
        {
            Debug.LogError(this.gameObject.name + "needs at least (1) active EventSystem.");
        }
    }

    public void OnEnable()
    {
        _eventSystem.SetSelectedGameObject(OnActiveSelection);
    }
}
