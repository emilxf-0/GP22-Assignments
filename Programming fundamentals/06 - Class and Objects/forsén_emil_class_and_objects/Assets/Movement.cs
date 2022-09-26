using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ProcessingLite.GP21
{
    Player myPlayer;
    Player myNewPlayer;
    Ball[] myBalls;

    public float accelerationSpeed;

    bool wrapRight;
    bool wrapLeft;

    public Vector2 axis;

    void Start()
    {
        myPlayer = new Player(Width / 2, Height / 2, 1.5f);
        myBalls = new Ball[10];

        myPlayer.acceleration = new Vector2(7, 2);
        myPlayer.deceleration = new Vector2(3, 2);

        for (int i = 0; i < myBalls.Length; i++)
        {
            myBalls[i] = new Ball(Random.Range(3f, 5f), Random.Range(3f, 5f), Random.Range(0.5f, 2f));
        }

    }

    void Update()
    {
        Background(50, 50, 120);
        
        // Player
        myPlayer.Draw();
        myPlayer.MaxVelocity();
        myPlayer.UpdatePosition();
        Wrap(myPlayer.position); 

        // Balls
        for (int i = 0; i < myBalls.Length; i++)
        {
            myBalls[i].Draw();
            myBalls[i].UpdatePos();
            myBalls[i].CheckCollision();

            // Trigger the Game Over screen
            if (CollisionDetection(myPlayer, myBalls[i]))
            {
                GetComponent<Movement>().enabled = false;
                GetComponent<GameOver>().enabled = true;
            }
        }
    }
   
    bool CollisionDetection(Player player, Ball ball)
    {
        float maxDistance = player.radius + ball.radius;

        if (Mathf.Abs(player.position.x - ball.position.x ) > maxDistance || Mathf.Abs(player.position.y - ball.position.y) > maxDistance)
        {
            return false;
        }
        else if (Vector2.Distance(player.position, ball.position) > maxDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    void Wrap(Vector2 vector)
    {
        if (vector.x >= Width)
        {
            wrapRight = true;
        }

        if (vector.x <= 0)
        {
            wrapLeft = true;
        }

        if (wrapRight)
        {
            myNewPlayer = new Player(vector.x - Width, vector.y, myPlayer.size);
            myNewPlayer.Draw();
        }

        if (wrapLeft)
        {
            myNewPlayer = new Player(vector.x + Width, vector.y, myPlayer.size);
            myNewPlayer.Draw();
        }

        if (vector.y + myPlayer.radius >= Height)
        {
            myPlayer.position.y = Height - myPlayer.radius;
        }

        if (vector.y - myPlayer.radius < 0)
        {
            myPlayer.position.y = 0 + myPlayer.radius;
        }
    }
}