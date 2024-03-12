using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFam_Manager : MonoBehaviour
{
    public Transform originalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_DrFAM"))
        {
            for (int i = 0; i < DrFam_CORE.instance.sitSpace.Length; i++)
            {
                if (!DrFam_CORE.instance.chairOccupied[i])
                {
                    DrFam_CORE.instance.chairOccupied[i] = true;

                    other.GetComponent<NPCController_DrFam>().agent.enabled = false;
                    other.GetComponent<NPCController_DrFam>().anim.SetBool("isSitting", true);
                    other.GetComponent<NPCController_DrFam>().agent.velocity = Vector3.zero;
                    other.transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.sitSpace[i].transform.eulerAngles.y, 0f);

                    other.transform.position = DrFam_CORE.instance.sitSpace[i].transform.position;

                    other.GetComponent<NPCController_DrFam>().occupiedChairIndex = i;

                    break;
                }
                else if (i == DrFam_CORE.instance.sitSpace.Length - 1)
                {
                    other.transform.position = originalPosition.position; 
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
