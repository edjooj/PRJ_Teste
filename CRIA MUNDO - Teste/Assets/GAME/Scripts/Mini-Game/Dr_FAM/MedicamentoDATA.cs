using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMedicamento", menuName = "Scriptable/Medicamento")]
public class MedicamentoDATA : ScriptableObject
{
    public string medicamentoName;
    public Sprite medicamentoImg;
    public GameObject medicamentoObj;
}
