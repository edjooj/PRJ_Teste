using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void ToggleCanvas()
    {
        // Inverte o estado do Canvas (ativo/inativo)
        canvas.enabled = !canvas.enabled;
    }
}
