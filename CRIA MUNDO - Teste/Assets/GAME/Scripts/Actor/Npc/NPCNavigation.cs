using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    public bool Ativado; 
    public List<Transform> PathPoints = new List<Transform>();
    

    private int index = 0;
    public float minDistance = 2;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent < Animator>();

       
        foreach (Transform child in PATH.transform)
        {
            PathPoints.Add(child);
        }
    }

    void Update()
    {
        if (Ativado)
        {
            roam();
        }
    }

    void roam()
    {
        
        if (Vector3.Distance(transform.position, PathPoints[index].position) < minDistance)
        {
            if (index >= 0 && index < PathPoints.Count - 1)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
        }

        agent.SetDestination(PathPoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped && agent.hasPath ? 1 : 0); 
    }
}
