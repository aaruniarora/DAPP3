using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeScenes : MonoBehaviour
{
    public void LoadNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
