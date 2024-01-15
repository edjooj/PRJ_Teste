using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI tempoText;
    private float tempoDecorrido = 0f;
    public bool cronometroAtivo = true;

    public int minutos;
    public int segundos;

    private void OnEnable()
    {
        tempoDecorrido = 0f;
    }

    private void Update()
    {
        if (cronometroAtivo)
        {
            tempoDecorrido += Time.deltaTime;
            AtualizarTempo();
        }
    }

    private void AtualizarTempo()
    {
        minutos = Mathf.FloorToInt(tempoDecorrido / 60f);
        segundos = Mathf.FloorToInt(tempoDecorrido % 60f);

        tempoText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    public void IniciarCronometro()
    {
        cronometroAtivo = true;
    }

    public void PararCronometro()
    {
        cronometroAtivo = false;
    }

    public void SalvarTempoNoFinalDaFase()
    {
        PlayerPrefs.SetFloat("TempoDecorrido", tempoDecorrido);
    }

    public void ReiniciarCronometro()
    {
        tempoDecorrido = 0f;
        AtualizarTempo();
    }
}
