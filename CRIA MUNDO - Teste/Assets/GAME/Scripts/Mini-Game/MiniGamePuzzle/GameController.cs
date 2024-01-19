using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject bolaSelecionada;



    // Método chamado quando o tubo é clicado
    public void OnTubeClicked(GameObject tubo)
    {
        if (bolaSelecionada != null)
        {
            // Move a bola selecionada para a posição do tubo clicado
            MoverBolaParaTubo(bolaSelecionada, tubo);

            // Limpa a seleção da bola após o movimento
            bolaSelecionada = null;

            // Adicione aqui qualquer outra lógica relacionada à interação do tubo
        }
    }

    // Função chamada quando a bola é clicada
    public void OnBallClicked(GameObject ball)
    {
        if (bolaSelecionada == null)
        {
            bolaSelecionada = ball;
        }
        else
        {
            // Se uma bola já estiver selecionada, mova a bola para o tubo clicado
            MoverBolaParaTubo(bolaSelecionada, GetTuboClicado());
            bolaSelecionada = null;
        }
    }

    // Função para obter o tubo clicado (substitua conforme necessário)
    private GameObject GetTuboClicado()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Verifica se o objeto atingido é um tubo
            if (hit.collider.CompareTag("Tubo"))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }


    // Função para mover a bola para o tubo selecionado
    private void MoverBolaParaTubo(GameObject bola, GameObject tubo)
    {
        // Verifica se o tubo está vazio ou se a bola pode ser colocada no topo
        if (tubo.transform.childCount == 0 || PodeAdicionarNoTopo(tubo, bola))
        {
            // Move a bola para a posição do tubo
            StartCoroutine(MoverBolaCoroutine(bola.transform, tubo.transform.position, 1f));

            // Configura a bola como filha do tubo
            bola.transform.parent = tubo.transform;
        }
        else
        {
            // Lógica adicional se o tubo não puder receber a bola (por exemplo, mensagem de erro)
            Debug.Log("Não é possível adicionar a bola ao tubo!");
        }
    }

    // Função para verificar se a bola pode ser adicionada ao topo do tubo
    private bool PodeAdicionarNoTopo(GameObject tubo, GameObject bola)
    {
        // Verifica se a bola é a próxima na sequência esperada
        int index = tubo.transform.childCount;
        if (index > 0)
        {
            BallController ballController = bola.GetComponent<BallController>();
            if (ballController != null)
            {
                BallType expectedType = GetExpectedType(index); // Substitua com sua lógica de obtenção do tipo esperado
                return ballController.GetBallType() == expectedType;
            }
        }

        return true; // Se o tubo estiver vazio, qualquer bola pode ser adicionada
    }

    // Função para obter o tipo de bola esperado na próxima posição do tubo
    private BallType GetExpectedType(int index)
    {
        // Substitua com a sua lógica para determinar o tipo esperado com base no índice
        // Pode ser uma lista predefinida, um padrão específico, etc.
        // Exemplo simples: alternar entre RED e BLUE
        return (index % 2 == 0) ? BallType.RED : BallType.BLUE;
    }

    private System.Collections.IEnumerator MoverBolaCoroutine(Transform bolaTransform, Vector3 targetPosition, float duration)
    {
        float elapsed = 0f;
        Vector3 startPosition = bolaTransform.position;

        while (elapsed < duration)
        {
            bolaTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bolaTransform.position = targetPosition; // Garanta que a posição final seja exata
    }
    public enum BallType
    {
        RED,
        GREEN,
        BLUE,
        ORANGE,
        BROWN
    }

}
