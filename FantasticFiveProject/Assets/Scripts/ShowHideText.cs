using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowHideText : MonoBehaviour
{
    public TMP_Text DisplayedText;
    public Button DisplayedButton;
    //public TMP_Text DisplayedButtonText;
 
    public void Display()
    {
        DisplayedText.enabled = true;
        DisplayedButton.gameObject.SetActive(true);
        //DisplayedButtonText.enabled = true;
    }

    void Start()
    {
        DisplayedText.enabled = false; 
        DisplayedButton.gameObject.SetActive(false);
        //DisplayedButtonText.enabled = false;
    }
}
