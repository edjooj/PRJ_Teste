using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Porta : MonoBehaviour
{
    public TextMeshProUGUI hudText;
    private float currentValue = 0;  // Valor inicial da HUD

    // Configuração da porta
    public char operation = '+';
    public float operand = 5;

    private void Start()
    {
        UpdateHUD();  // Atualiza a HUD com o valor inicial
    }

    private void OnTriggerEnter(Collider other)
    {
        PerformOperation();
    }

    private void PerformOperation()
    {
        switch (operation)
        {
            case '+':
                currentValue += operand;
                break;
            case '-':
                currentValue -= operand;
                break;
            case '*':
                currentValue *= operand;
                break;
            case '/':
                if (operand != 0)
                {
                    currentValue /= operand;
                }
                else
                {
                    Debug.LogWarning("Tentativa de divisão por zero!");
                }
                break;
            default:
                Debug.LogWarning("Operação inválida!");
                break;
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        // Atualize o texto da HUD com o valor atualizado
        hudText.text = "Value: " + currentValue.ToString("F2");  // Exibe o valor com duas casas decimais
    }
}
