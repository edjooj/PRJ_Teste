using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController playerController;

    public void JumpFunction()
    {
        playerController.Jump();
    }
}
