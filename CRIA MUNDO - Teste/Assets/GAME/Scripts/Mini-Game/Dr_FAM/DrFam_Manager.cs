using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFam_Manager : MonoBehaviour
{
    public GameObject[] sitSpace;
    private bool[] chairOccupied;
    public Transform originalPosition;



    //Se o NPC entrar no trigger ele ocupar um dos sitSpace disponivel e desativar o NavMeshAgent e mudar para anim de sentado e esperar um tempo para sair

    //Se o NPC sair do trigger ele liberar o sitSpace e ativar o NavMeshAgent e mudar para anim de andando

    private void Start()
    {
        chairOccupied = new bool[sitSpace.Length];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_DrFAM"))
        {
            for (int i = 0; i < sitSpace.Length; i++)
            {
                if (!chairOccupied[i])
                {
                    chairOccupied[i] = true;

                    other.GetComponent<NPCController_DrFam>().agent.enabled = false;
                    other.GetComponent<NPCController_DrFam>().anim.SetBool("isSitting", true);
                    other.GetComponent<NPCController_DrFam>().agent.velocity = Vector3.zero;
                    other.transform.rotation = Quaternion.Euler(0f, sitSpace[i].transform.eulerAngles.y, 0f);

                    other.transform.position = sitSpace[i].transform.position;

                    break;
                }
                else if (i == sitSpace.Length - 1)
                {
                    other.transform.position = originalPosition.position; 
                    Destroy(other.gameObject);
                }
            }
        }
    }


    IEnumerator WaitToLeave(GameObject npc, int i)
    {
        yield return new WaitForSeconds(20);
        sitSpace[i].SetActive(false);
        npc.GetComponent<NPCController_DrFam>().agent.enabled = true;
        npc.GetComponent<NPCController_DrFam>().anim.SetBool("isSitting", false);
    }







}
