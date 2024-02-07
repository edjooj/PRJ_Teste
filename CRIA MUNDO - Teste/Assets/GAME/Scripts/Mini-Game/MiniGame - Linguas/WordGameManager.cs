using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordGameManager : MonoBehaviour
{
    public GameControl[] levels; 

    public List<GameObject> letters; 

    public GameObject letterPrefab;
    public GameObject letterPrefabSemBotão;

    public int currentLevel; 

    public TMP_InputField word;

    public GameObject wordPrefab;
    public Transform wordTransform;

    public int wordsRevealed;
    public GameObject hudFim;

    [Header("Placar")]
    public string tempo;
    public int clicks;
    public int limpar;

    [Header("Cronometro")]
    private float tempoDecorrido = 0f;
    public bool cronometroAtivo = true;

    public int minutos;
    public int segundos;

    private void OnEnable()
    {
        currentLevel = ScoreboardController.instance.currentFaseLinguas;
        SetLettersForCurrentLevel();
        GenerateWords();
        word.text = "";
        wordsRevealed = 0;
        tempoDecorrido = 0f;
    }


    #region Cronometro

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

        tempo = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    #endregion

    public void SetLettersForCurrentLevel()
    {
        if (currentLevel < 0 || currentLevel >= levels.Length)
        {
            Debug.LogError("CurrentLevel está fora do intervalo!");
            return;
        }

        GameControl currentGameControl = levels[currentLevel];

        int totalLettersNeeded = 0;
        foreach (WordControl wordControl in currentGameControl.Levels)
        {
            totalLettersNeeded += wordControl.letters.Count;
        }

        if (letters.Count < totalLettersNeeded)
        {
            Debug.LogError("Não há GameObjects suficientes na lista 'letters' para as letras do nível atual.");
            return;
        }

        int currentLetterIndex = 0;

        foreach (WordControl wordControl in currentGameControl.Levels)
        {
            foreach (string letter in wordControl.letters)
            {
                if (currentLetterIndex < letters.Count)
                {
                    GameObject letterContainer = letters[currentLetterIndex];

                    GameObject letterGO = Instantiate(letterPrefab, letterContainer.transform);
                    letterGO.GetComponentInChildren<TextMeshProUGUI>().text = letter;

                    currentLetterIndex++;
                }
            }
        }
    }
    public void Compled()
    {
        ScoreboardController.instance.UpdateLinguasPointsInFirebase(tempo, clicks, limpar, currentLevel);
    }

    private void GenerateWords()
    {
        GameControl currentGameControl = levels[currentLevel];

        foreach (WordControl wordControl in currentGameControl.Levels)
        {
            foreach (string dictionaryWord in wordControl.dictionary)
            {
                GameObject wordGO = Instantiate(wordPrefab, wordTransform);
                wordGO.name = dictionaryWord; 

                foreach (char c in dictionaryWord)
                {
                    GameObject letterGO = Instantiate(letterPrefabSemBotão, wordGO.transform);
                    TextMeshProUGUI letterText = letterGO.GetComponentInChildren<TextMeshProUGUI>();
                    letterText.text = c.ToString();

                    Color color = letterText.color;
                    color.a = 0;
                    letterText.color = color;
                }
            }
        }
    }

    public void Limpar()
    {
        word.text = "";
        limpar++;
    }

    public void CheckWord()
    {
        GameControl currentGameControl = levels[currentLevel];

        foreach (WordControl wordControl in currentGameControl.Levels)
        {
            if (wordControl.dictionary.Contains(word.text))
            {
                RevealWord(word.text);
                word.text = "";
                Debug.Log("Acertou!");
                return;

            }
        }
        Debug.Log("Errou!");
    }

    private void RevealWord(string correctWord)
    {
        Transform wordGO = wordTransform.Find(correctWord);
        if (wordGO != null)
        {
            foreach (Transform letter in wordGO)
            {
                TextMeshProUGUI letterText = letter.GetComponentInChildren<TextMeshProUGUI>();
                if (letterText != null)
                {
                    Color color = letterText.color;
                    color.a = 1; 
                    letterText.color = color;
                }
            }

            wordsRevealed++;

          
            if (wordsRevealed == 7 )
            {
                hudFim.SetActive(true);
                Mudarfase();

            }

        }
    }

    public void Mudarfase()
    {
        currentLevel++;
        Compled();
    }


}
