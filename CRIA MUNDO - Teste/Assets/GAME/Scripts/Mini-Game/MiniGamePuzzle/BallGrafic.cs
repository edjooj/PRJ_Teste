using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphic : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Game.BallType type)
    {
        switch (type)
        {
            case Game.BallType.RED:
                sprintRenderer.color = Color.red;
                break;
            case Game.BallType.GREEN:
                sprintRenderer.color = Color.green;
                break;
        }
    }
}
