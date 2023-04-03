using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCanvas : MonoBehaviour
{
    public GameObject nextCanvas;
    //public GameObject currCanvas;

    void Start()
    {
        // Add a listener to the button's click event
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Enable the canvas when the button is clicked
        nextCanvas.SetActive(true);
        //currCanvas.SetActive(false);
    }
}
