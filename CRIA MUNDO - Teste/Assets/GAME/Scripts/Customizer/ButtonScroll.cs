using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonScroll : MonoBehaviour
{
    [System.Serializable]
    public class ButtonScrollPair
    {
        public Button button;
        public ScrollRect scrollRect;
    }

    public List<ButtonScrollPair> buttonScrollPairs;

    void Start()
    {
        
        foreach (var pair in buttonScrollPairs)
        {
            pair.scrollRect.gameObject.SetActive(false);

            
            pair.button.onClick.AddListener(() => SelecionarScroll(pair.scrollRect));
        }
    }

    void SelecionarScroll(ScrollRect scrollRect)
    {
       
        foreach (var pair in buttonScrollPairs)
        {
            pair.scrollRect.gameObject.SetActive(false);
        }

        
        scrollRect.gameObject.SetActive(true);
    }
}





