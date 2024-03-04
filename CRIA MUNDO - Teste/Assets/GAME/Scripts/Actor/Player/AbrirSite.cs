using UnityEngine;
using UnityEngine.UI;

public class AbrirLink : MonoBehaviour
{
    public string url;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => AbrirURL(url));
        }
    }

    private void AbrirURL(string link)
    {
        Application.OpenURL(link);
    }
}
