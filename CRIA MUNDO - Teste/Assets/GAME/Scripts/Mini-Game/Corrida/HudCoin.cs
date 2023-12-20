using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudCoin : MonoBehaviour
{
    public TextMeshProUGUI TextCoin;
    public int valorAtual = 100;

    private void Start()
    {
        AtualizarHUD();
    }

    public void AdicionarValor(int valor)
    {
        valorAtual += valor;
        AtualizarHUD();
    }

    public void SubtrairValor(int valor)
    {
        valorAtual -= valor;
        AtualizarHUD();
    }

    public void MultiplicarValor(float valor)
    {
        valorAtual = Mathf.RoundToInt(valorAtual * valor);
        AtualizarHUD();
    }

    public void DividirValor(float valor)
    {
        if (valor != 0)
        {
            valorAtual = Mathf.RoundToInt(valorAtual / valor);
            AtualizarHUD();
        }
        else
        {
            Debug.LogWarning("Tentativa de divisão por zero. Operação ignorada.");
        }
    }

    private void AtualizarHUD()
    {
        TextCoin.text = "Valor Atual: " + valorAtual;
    }
}
