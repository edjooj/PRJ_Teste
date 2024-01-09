using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockIconController : MonoBehaviour
{
    public PlayerIconData playerData;

    void Start()
    {
        playerData = new PlayerIconData();
    }

    public void UnlockIcon(IconsDATA iconData)
    {
        playerData.AddIcon(iconData);
        playerData.SaveIconData(iconData);
    }
}


