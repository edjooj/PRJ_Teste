using Photon.Pun.Demo.PunBasics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController_DrFam : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;
    public SpriteRenderer spriteRenderer;

    public float timeToTake = 5f;
    public float transitionDuration = 10f;

    public float atendimentoRange;
    public LayerMask playerLayer;
    bool[] camasOcupadas = new bool[DrFam_CORE.instance.camasDisponiveis.Length];


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target_DrFAM").transform;
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no NPC!");
        }
    }
    private void FixedUpdate()
    {
        SeeThePlayer();
    }

    void SeeThePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, atendimentoRange, playerLayer);
        Debug.Log(hits.Length);
        Debug.Log(hits[0].name);
        if (hits.Length >= 1)
        {
            StartCoroutine(TransitionRemovedSegment(0f, 3.17f, transitionDuration));
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atendimentoRange);
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
        if(other.CompareTag("Player"))
        {
            StartCoroutine(TransitionRemovedSegment(0f, 3.17f, transitionDuration));
        }
    }

    IEnumerator TransitionRemovedSegment(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.material.SetFloat("_RemovedSegment", endValue);

        int camaDisponivelIndex = -1;
        for (int i = 0; i < camasOcupadas.Length; i++)
        {
            if (!camasOcupadas[i])
            {
                camaDisponivelIndex = i;
                break;
            }
        }

        if (camaDisponivelIndex != -1)
        {
            // Se houver uma cama disponível, deite-se nela
            agent.enabled = false;
            anim.SetBool("isSitting", false);
            anim.SetBool("isLaying", true);
            agent.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.camasDisponiveis[camaDisponivelIndex].transform.eulerAngles.y, 0f);
            transform.position = DrFam_CORE.instance.camasDisponiveis[camaDisponivelIndex].transform.position;
            camasOcupadas[camaDisponivelIndex] = true;
            StartCoroutine(WaitToLeave(gameObject, camaDisponivelIndex));
        }
        else
        {
            // Se não houver cama disponível, continue sentado
            agent.enabled = false;
            anim.SetBool("isSitting", true);
            agent.velocity = Vector3.zero;
        }
    }

    IEnumerator WaitToLeave(GameObject npc, int i)
    {
        yield return new WaitForSeconds(timeToTake);
        DrFam_CORE.instance.camasDisponiveis[i].SetActive(false);
        agent.enabled = true;
        anim.SetBool("isSitting", false);
    }
}
