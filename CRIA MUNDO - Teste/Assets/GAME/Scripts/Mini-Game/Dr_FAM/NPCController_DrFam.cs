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

    public float dialogueRange;
    public LayerMask playerLayer;


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
        Collider[] hits = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);
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
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
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

        for (int i = 0; i < DrFam_CORE.instance.camasDisponiveis.Length; i++)
        {
            if (!DrFam_CORE.instance.camasDisponiveis[i].activeInHierarchy)
            {
                //Se tiver, ele vai se seitar nela
                agent.enabled = false;
                anim.SetBool("isSitting", true);
                agent.velocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.camasDisponiveis[i].transform.eulerAngles.y, 0f);
                transform.position = DrFam_CORE.instance.camasDisponiveis[i].transform.position;
                DrFam_CORE.instance.camasDisponiveis[i].SetActive(true);
                StartCoroutine(WaitToLeave(gameObject, i));
                break;
            }
            else if (i == DrFam_CORE.instance.camasDisponiveis.Length - 1)
            {
                //Se não tiver, ele continuará sentado
                agent.enabled = false;
                anim.SetBool("isSitting", true);
                agent.velocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.camasDisponiveis[i].transform.eulerAngles.y, 0f);
                transform.position = DrFam_CORE.instance.camasDisponiveis[i].transform.position;
                StartCoroutine(WaitToLeave(gameObject, i));
            }
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
