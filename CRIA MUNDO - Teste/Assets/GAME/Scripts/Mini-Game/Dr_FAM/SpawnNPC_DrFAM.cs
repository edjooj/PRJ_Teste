using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SpawnNPC_DrFAM : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] NPCPrefabs;
    public Transform target;
    public float timeToSpawn;
    private bool startMiniGame = false;

    private Coroutine spawnCoroutine;

    IEnumerator SpawnNPC()
    {
        while (startMiniGame)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randomNPC = Random.Range(0, NPCPrefabs.Length);
            GameObject NPC = Instantiate(NPCPrefabs[randomNPC], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            NPCController_DrFam npcController = NPC.GetComponent<NPCController_DrFam>();
            npcController.target = target;
            npcController.MoveToTarget();
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    public void StartMiniGame()
    {
        if (!startMiniGame)
        {
            startMiniGame = true;
            spawnCoroutine = StartCoroutine(SpawnNPC());
        }
    }

    public void StopMiniGame()
    {
        if (startMiniGame)
        {
            startMiniGame = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            GameObject[] NPCDrFam = GameObject.FindGameObjectsWithTag("NPC_DrFAM");
            foreach (var npc in NPCDrFam)
            {
                Destroy(npc);
            }
        }
    }
}
