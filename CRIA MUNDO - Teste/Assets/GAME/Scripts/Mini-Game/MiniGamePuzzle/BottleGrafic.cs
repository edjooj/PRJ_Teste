using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGrafic : MonoBehaviour
{

    public BallGrafic[] ballGrafics;
   public void SetGraphic(Game.BallType[] ballTypes)
    {
        for(int i = 0;i < ballGrafics.Lenght; i++)
        {
            if(i >= ballTypes.Length)
            {
                ballGrafics[i].SetColor(BallGraphicType.NONE);
            }
            else
            {

                BallGraphicType type = BallGraphicType.RED;

                switch(ballTypes[i])
                {
                    case Game.BallType.RED:
                        type = BallGraphicType.RED;
                        break;
                    case Game.BallType.GREEN:
                        type = BallGraphicType.GREEN;
                        break;
                }
            }
        }
    }
}
