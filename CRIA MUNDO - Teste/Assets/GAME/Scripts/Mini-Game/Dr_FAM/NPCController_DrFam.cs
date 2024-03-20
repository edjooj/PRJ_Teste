using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController_DrFam : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform target;
    public Vector3 inicialPosition; //Para o NPC retornar ao ponto inicial apos medicado
    public SpriteRenderer spriteRenderer; 
    public MedicamentoDATA[] medicamentos;
    public SpriteRenderer medicamentoSprite;
    public ParticleSystem happy;

    public float timeToTake = 5f;
    public float transitionDuration = 10f;

    public float atendimentoRange;
    public LayerMask playerLayer;

    private Coroutine courotine;
    public int occupiedChairIndex = -1;

    public MedicamentoDATA medicamentoEscolhido;
    public PlayerDrFam_Controller player;
    public bool playerDeitado = false;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target_DrFAM").transform;
        inicialPosition = transform.position;
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
            player = hits[0].GetComponent<PlayerDrFam_Controller>();
            courotine = StartCoroutine(StartTimer());
        }
        else if (hits.Length <= 0 && courotine != null)
        {
            StopCoroutine(courotine);
            courotine = null;
        }

        if (!agent.isOnNavMesh)
        {
            TeleportToNearestNavMesh();
        }

    }

    private void TeleportToNearestNavMesh()
    {
        Vector3 validPosition = FindNearestNavMeshPoint(transform.position);
        transform.position = validPosition;
    }

    private Vector3 FindNearestNavMeshPoint(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 100f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            Debug.LogWarning("Não foi possível encontrar um ponto válido no NavMesh. Retornando a posição original.");
            return position;
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
        int foundIndex = -1;

        if (playerDeitado)
        {
            for (int i = 0; i < player.medicamentosEmpilhados.Count; i++)
            {
                MedicamentoDATA medicamento = player.medicamentosEmpilhados[i];
                if (medicamento == medicamentoEscolhido)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex != -1)
            {
                while (elapsedTime < transitionDuration)
                {
                    float newValue = Mathf.Lerp(0f, 3.17f, elapsedTime / transitionDuration);
                    spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                this.agent.enabled = true;
                this.anim.SetBool("isSitting", false);
                this.anim.SetBool("isLaying", false);
                this.agent.velocity = Vector3.zero;
                happy.Play();

                this.transform.rotation = Quaternion.Euler(0f, DrFam_CORE.instance.camasDisponiveis[foundIndex].transform.eulerAngles.y, 0f);

                agent.SetDestination(inicialPosition);

                player.medicamentosEmpilhados.RemoveAt(foundIndex);

                GameObject medicamentoObj = medicamentoEscolhido.medicamentoObj;
                medicamentoEscolhido.medicamentoObj = null;

                Destroy(medicamentoObj);
            }
        }
        else
        {
            while (elapsedTime < transitionDuration)
            {
                float newValue = Mathf.Lerp(0f, 3.17f, elapsedTime / transitionDuration);
                spriteRenderer.material.SetFloat("_RemovedSegment", newValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            DeitarNaCama();
        }
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
                    playerDeitado = true;
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
            medicamentoSprite.sprite = medicamentos[randomMedicine].medicamentoImg;
            if (medicamentoEscolhido == null)
            {
                medicamentoEscolhido = medicamentos[randomMedicine];
            }
        }
}