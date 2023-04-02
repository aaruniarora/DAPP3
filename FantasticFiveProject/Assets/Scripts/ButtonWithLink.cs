using UnityEngine;
using UnityEngine.UI;

public class ButtonWithLink : MonoBehaviour
{
    public string url;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OpenUrl);
    }

    void OpenUrl()
    {
        Application.OpenURL(url);
    }
}
