using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawRect : MonoBehaviour
{
    void Start()
    {
        Texture2D texture = new Texture2D(128, 128);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
    }
}