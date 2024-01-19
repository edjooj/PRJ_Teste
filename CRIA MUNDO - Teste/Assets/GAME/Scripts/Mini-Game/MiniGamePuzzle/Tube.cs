using UnityEngine;

public class TubeController : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        // Obtém a referência para o controlador do jogo no início
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnMouseDown()
    {
        // Informa ao controlador do jogo que este tubo foi clicado
        gameController.OnTubeClicked(gameObject);
    }
}
