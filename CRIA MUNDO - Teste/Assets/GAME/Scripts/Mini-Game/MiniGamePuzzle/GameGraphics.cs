using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGraphics : MonoBehaviour
{
    public List<BottleGrafic> bottleGraphics;

    public void  RefreshBottleGraphics(List<Game.Bottle> bottles)
    {
        for(int = 0; i < bottles.Count; i++)
        {
            Game.Bottle gb = bottles[i];
            BottleGrafic bottleGraphic = bottleGraphics[i];

            List<Game.BallType> ballTypes = new List<Game.BallType>();

            foreach(var ball in gb.balls )
            {
                ballTypes.Add( ball.type );
            }

            bottleGraphic.SetGraphic(ballTypes.ToArray());
        }
    }
}
