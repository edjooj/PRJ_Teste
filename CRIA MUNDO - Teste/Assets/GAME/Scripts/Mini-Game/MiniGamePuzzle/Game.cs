using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Game : MonoBehaviour
{

    public List<Bottle> bottles;


    private void Start()
    {
        bottles = new List<Bottle>(); 

        bottles.Add(new Bottle 
        {
            balls = new List<Ball> { new Ball { type = BallType.GREEN }, new Ball { type = BallType.RED } }
        });

        bottles.Add(new Bottle
        {
            balls = new List<Ball>()
        });

        SwitchBall(bottles[0], bottles[1]);
        PrintBottles();
    }

    public void PrintBottles()
    {   
        Debug.Log("Bottles=====");
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < bottles.Count; i++)
        {
            Bottle b = bottles[i];
            sb.Append("Bottle " + (i+1)+ ":");
            foreach(Ball ball in b.balls)
            {
                sb.Append(" " + ball.type);
                sb.Append(",");
            }
            Debug.Log(sb.ToString());
            sb.Clear();

        }
    }

    public void SwitchBall(Bottle bottle1, Bottle bottle2)
    {
        List<Ball> bottle1Balls = bottle1.balls;
        List<Ball> bottle2Balls = bottle2.balls;

        Ball.b = bottle1Balls[0];
        bottle1Balls.RemoveAt(0);

        bottle2balls.Add(b);
    }

    public class Bottle
    {
        public List<Ball> balls = new List<Ball>();
    }

    public class Ball
    {
        public BallType type;
    }

    public enum BallType
    {
        RED,
        GREEN, 
        BLUE,
        ORANGE,
        BROWN
    }

}
