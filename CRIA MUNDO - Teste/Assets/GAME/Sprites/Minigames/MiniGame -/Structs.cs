using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WordControl
{
    public List<string> dictionary;
    public List<string> letters;
}

[System.Serializable]
public struct GameControl
{
    public WordControl[] Levels;
    public int CurrentLevel;
}
