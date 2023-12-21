using UnityEngine;
using System.Collections;

public class OptionManager : MonoBehaviour
{
    public float inversionProbability = 0.5f;

    private void Start()
    {
        if (Random.value < inversionProbability)
        {
            InvertOptionsInAllStages();
        }
    }

    private void InvertOptionsInAllStages()
    {
        Stage[] stages = FindObjectsOfType<Stage>();
        foreach (Stage stage in stages)
        {
            stage.InvertOptions();
        }
    }
}
