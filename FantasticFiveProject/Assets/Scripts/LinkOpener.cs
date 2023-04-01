using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkOpener : MonoBehaviour
{
    public string url;

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
    
}

