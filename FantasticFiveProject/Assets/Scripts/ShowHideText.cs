using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowHideText : MonoBehaviour
{
    public TMP_Text DisplayedText; //This is the answer before the link
    public Button DisplayedButton; //This is the Imperial Link (it's a button)
    [SerializeField] Button[] buttons; //The option buttons
 
 
    //Initailly the Imperial link and answer are invisible
    void Start()
    {
        DisplayedText.enabled = false; 
        DisplayedButton.gameObject.SetActive(false);
    }


    //When any option is clicked the answer is displayed 
    //and the options are disabled
    public void Display() 
    {
        DisplayedText.enabled = true; 
        DisplayedButton.gameObject.SetActive(true);
        for (int i=0; i<buttons.Length; i++)
        {
            //buttons[i].SetActive(false);
            buttons[i].enabled = false;
        }
    }

}
