using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Scriptable/New Dialogue", fileName = "New Dialog")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor; //Qual o NPC está falando

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentences;

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences // Informações de quem está falando
{
    public string actorName;  // Nome do NPC
    public Sprite profile;    // Imagem do NPC
    public Language sentence; // Lingua na qual o NPC está falando
}

[System.Serializable]
public class Language
{
    public string ptBR;
    public string enUS;
    public string esES;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target;
        Language l = new Language();
        l.ptBR = ds.sentences;

        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = l;

        if (GUILayout.Button("Add Sentence"))
        {
            if (ds.sentences != "")
            {
                ds.dialogues.Add(s);

                ds.speakerSprite = null;
                ds.sentences = "";
            }

        }
    }
}

#endif
