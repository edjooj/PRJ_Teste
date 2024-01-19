using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [Header("Placar")]
    public int clicks;
    public int limpar;

    private void Start()
    {
        SetLettersForCurrentLevel();
        GenerateWords();
        word.text = "";
    }

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

    private void GerarPalavras()
    {
        GameControl currentGameControl = levels[currentLevel];
        int currentwordsIndex = 0;

        foreach (WordControl wordControl in currentGameControl.Levels)
        {
            foreach (string letter in wordControl.letters)
            {
                if (currentwordsIndex < letters.Count)
                {
                    GameObject letterContainer = letters[currentwordsIndex];

                    GameObject letterGO = Instantiate(letterPrefabSemBotão, letterContainer.transform);
                    letterGO.GetComponentInChildren<TextMeshProUGUI>().text = letter;

                    currentwordsIndex++;
                }
            }
        }
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
        // Encontre o GameObject da palavra correspondente
        Transform wordGO = wordTransform.Find(correctWord);
        if (wordGO != null)
        {
            // Altere o alpha de cada letra para 1
            foreach (Transform letter in wordGO)
            {
                TextMeshProUGUI letterText = letter.GetComponentInChildren<TextMeshProUGUI>();
                if (letterText != null)
                {
                    Color color = letterText.color;
                    color.a = 1; // 100% de opacidade
                    letterText.color = color;
                }
            }
        }
    }


}
