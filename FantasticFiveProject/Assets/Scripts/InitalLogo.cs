using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitalLogo : MonoBehaviour
{
    public string nextSceneName;
    
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
