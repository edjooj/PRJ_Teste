using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControle : MonoBehaviour
{
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    public TextMeshProUGUI textPlayTime;
    [SerializeField]
    public TextMeshProUGUI textMoveCount;
    [SerializeField]
    private Board board;

    public void OnResultPanel()
    {
        resultPanel.SetActive(true);
        textPlayTime.text = $": {board.PlayTime / 60:D2} : {board.PlayTime % 60:D2}";
        textMoveCount.text = $": " + board.MoveCount;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

