using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonScroll : MonoBehaviour
{
    [System.Serializable]
    public class ButtonScrollPair
    {
        public Button button;
        public GameObject painel;
    }

    public List<ButtonScrollPair> buttonScrollPairs;

    void Start()
    {
        
        foreach (var pair in buttonScrollPairs)
        {
            pair.painel.gameObject.SetActive(false);

            
            pair.button.onClick.AddListener(() => SelecionarScroll(pair.painel));
        }
    }

    void SelecionarScroll(GameObject scrollRect)
    {
       
        foreach (var pair in buttonScrollPairs)
        {
            pair.painel.gameObject.SetActive(false);
        }

        
        scrollRect.gameObject.SetActive(true);
    }
}





