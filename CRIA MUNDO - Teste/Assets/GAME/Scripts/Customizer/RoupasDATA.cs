using UnityEngine;

[CreateAssetMenu(fileName = "RoupasDATA", menuName = "Scriptable/RoupasDATA")]
public class RoupasDATA : ScriptableObject
{
    public Color[] skinColor;

    public Mesh[] camisa;
    public Mesh[] calça;
    public Mesh[] luva;
    public Mesh[] sapato;
    public Mesh[] cabelo;
    public Mesh[] chapeu;
}
