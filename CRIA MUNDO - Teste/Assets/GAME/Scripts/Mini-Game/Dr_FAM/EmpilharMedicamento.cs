using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpilharMedicamento : MonoBehaviour
{
    public Transform spawnPointMedicamento; 
    public int AlturaMaxima = 5; 
    public float distanciaMedicamento = 1.0f; 

    private List<GameObject> medicamentosEmpilhados = new List<GameObject>();

    public void EmpilharObjeto(GameObject medicamentoSelecionado)
    {
        if (medicamentosEmpilhados.Count < 5) 
        {
            Vector3 spawnPosition = spawnPointMedicamento.position + Vector3.up * medicamentosEmpilhados.Count * distanciaMedicamento;
            GameObject newObject = Instantiate(medicamentoSelecionado, spawnPosition, Quaternion.identity);
            newObject.transform.parent = spawnPointMedicamento;
            medicamentosEmpilhados.Add(newObject);
        }
    }
}
