using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Icon", menuName ="Scriptable/New Icon")]
public class IconsDATA : ScriptableObject
{
    public Sprite iconSprite;
    public string iconName;
    public string iconDescription;

    public bool haveIcon = false;
}
