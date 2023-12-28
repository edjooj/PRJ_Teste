using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGame1 : MonoBehaviour
{
    public TextMeshProUGUI hudText;
    public TextMeshProUGUI fimText;
    public float currentValue = 1000;
    
    private void Start()
    {
       
        UpdateHUD();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PortaAdd"))
        {

            PerformOperation('+', 50);
        }
        else if (other.CompareTag("PortaSub"))
        {

            PerformOperation('-', 50);
        }
        else if (other.CompareTag("PortaMult"))
        {

            PerformOperation('*', 2);
        }
        else if (other.CompareTag("PortaDiv"))
        {

            PerformOperation('/', 2);
        }
       
    }

    private void PerformOperation(char operation, float operand)
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
        hudText.text = "Credito: " + currentValue.ToString("F2");
    }
}
