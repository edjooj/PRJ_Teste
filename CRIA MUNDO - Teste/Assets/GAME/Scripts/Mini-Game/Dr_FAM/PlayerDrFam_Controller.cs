using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrFam_Controller : MonoBehaviour
{
    public Transform spawnPointMedicamento; 
    public int AlturaMaxima = 5; 
    public float distanciaMedicamento = 1.0f; 

    public List<MedicamentoDATA> medicamentosEmpilhados = new List<MedicamentoDATA>();

    public void EmpilharObjeto(MedicamentoDATA medicamentoSelecionado)
    {
        if (medicamentosEmpilhados.Count < 5) 
        {
            Vector3 spawnPosition = spawnPointMedicamento.position + Vector3.up * medicamentosEmpilhados.Count * distanciaMedicamento;
            GameObject newObject = Instantiate(medicamentoSelecionado.medicamentoObj, spawnPosition, Quaternion.identity);
            newObject.transform.parent = spawnPointMedicamento;
            medicamentosEmpilhados.Add(medicamentoSelecionado);
        }
    }
}
