using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject trueOption; 
    public GameObject falseOption; 

    public void InvertOptions()
    {
        
        Vector3 tempPosition = trueOption.transform.position;
        trueOption.transform.position = falseOption.transform.position;
        falseOption.transform.position = tempPosition;
    }
}
