using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Version : MonoBehaviour
{
    public TMP_Text textbox;
    private void OnEnable()
    {
        textbox.text = Application.version;
    }
}
