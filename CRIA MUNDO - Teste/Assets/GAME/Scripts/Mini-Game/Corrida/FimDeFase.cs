using UnityEngine;
using UnityEngine.UI;

public class FimDeFase : MonoBehaviour
{
    public GameObject endLevelHUD;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        { 
            endLevelHUD.SetActive(true);
            PontuaçãoFinal();
        }
    }

    public void PontuaçãoFinal()
    {
        if(NetworkController.instance.scoreController.currentScorePoint > NetworkController.instance.scoreController.scorePoint)
        {
            NetworkController.instance.scoreController.UpdatePlayerPointsInFirebase(NetworkController.instance.scoreController.currentScorePoint);
        }
    }

}
