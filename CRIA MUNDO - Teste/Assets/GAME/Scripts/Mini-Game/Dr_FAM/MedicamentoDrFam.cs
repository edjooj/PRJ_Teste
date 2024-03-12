using System.Collections;
using UnityEngine;

public class MedicamentoDrFam : MonoBehaviour
{
    public float atendimentoRange;
    public LayerMask playerLayer;
    public GameObject medicamento;
    public EmpilharMedicamento empilharMedicamento;
    public SpriteRenderer spriteRenderer;
    public float transitionDuration = 10f;

    private Coroutine courotine;

    private void FixedUpdate()
    {
        SeeThePlayer();
    }

    private void SeeThePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, atendimentoRange, playerLayer);
        if (hits.Length >= 1 && courotine == null)
        {
            empilharMedicamento = hits[0].GetComponent<EmpilharMedicamento>();
            courotine = StartCoroutine(StartTimer());
        }
        else if (hits.Length <= 0 && courotine != null)
        {
            StopCoroutine(courotine);
            courotine = null;
        }
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

        Debug.Log("Medicamento empilhado" + medicamento);
        spriteRenderer.material.SetFloat("_RemovedSegment", 3.17f);
        empilharMedicamento.EmpilharObjeto(medicamento);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atendimentoRange);
    }
}
