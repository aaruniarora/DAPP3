using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHideText : MonoBehaviour
{
    public TMP_Text DisplayedText;
 
    public void ToggleText()
    {
        if (DisplayedText.enabled == true)
        {
            DisplayedText.enabled = false;
        }
        else
        {
            DisplayedText.enabled = true;
        }
    }

    void Start()
    {
        DisplayedText.enabled = false; 
    }
}
