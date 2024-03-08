using UnityEngine;
using UnityEngine.AI;

public class NPCController_DrFam : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target_DrFAM").transform;
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no NPC!");
        }
    }

    public void MoveToTarget()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
