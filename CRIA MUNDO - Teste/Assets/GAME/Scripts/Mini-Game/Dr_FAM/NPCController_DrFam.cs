using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController_DrFam : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;
    public SpriteRenderer spriteRenderer;
    public GameObject[] medicamentos;

    public float timeToTake = 5f;
    public float transitionDuration = 10f;

    public float atendimentoRange;
    public LayerMask playerLayer;

    private Coroutine courotine;
    public int occupiedChairIndex = -1;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target_DrFAM").transform;
    }

    private void FixedUpdate()
    {
        SeeThePlayer();
    }

    public void MoveToTarget()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void SeeThePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, atendimentoRange, playerLayer);
        if (hits.Length >= 1 && courotine == null)
        {
            courotine = StartCoroutine(StartTimer());
        }
        else if (hits.Length <= 0 && courotine != null)
        {
            StopCoroutine(courotine);
            courotine = null;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atendimentoRange);
    }

    private IEnumerator StartTimer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float newValue = Mathf.Lerp(0f, 3.17f, elapsedTime / transitionDuration);
            spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        DeitarNaCama();
    }

    void DeitarNaCama()
    {
        for (int i = 0; i < DrFam_CORE.instance.camasDisponiveis.Length; i++)
        {
            if (!DrFam_CORE.instance.camasOcupadas[i])
            {
                DrFam_CORE.instance.camasOcupadas[i] = true;

                this.agent.enabled = false;
                this.anim.SetBool("isSitting", false);
                this.anim.SetBool("isLaying", true);
                this.agent.velocity = Vector3.zero;
                this.transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.camasDisponiveis[i].transform.eulerAngles.y, 0f);

                this.transform.position = DrFam_CORE.instance.camasDisponiveis[i].transform.position;

                SelectMedicine();

                break;
            }
        }

        if (occupiedChairIndex != -1)
        {
            DrFam_CORE.instance.chairOccupied[occupiedChairIndex] = false;
            occupiedChairIndex = -1;
        }


    }

    void SelectMedicine()
    {
        int randomMedicine = Random.Range(0, medicamentos.Length);
        Instantiate(medicamentos[randomMedicine], transform.position, Quaternion.identity);
    }
}
