using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ProcessingLite.GP21
{
    Player myPlayer;
    Ball[] myBalls;

    bool gameOver = false;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!gameOver)
        {
            Background(10, 10, 30);

            // Player
            myPlayer.Draw();
            myPlayer.MaxVelocity();
            myPlayer.UpdatePosition();
            Wrap(myPlayer);
            Bounce(myPlayer);

            // Balls
            for (int i = 0; i < myBalls.Length; i++)
            {
                myBalls[i].Draw();
                myBalls[i].UpdatePos();
                myBalls[i].CheckCollision();

                // Trigger the Game Over screen
                if (CollisionDetection(myPlayer, myBalls[i]))
                {
                    gameOver = !gameOver;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        

        if (gameOver)
        {
            // Overlay
            Fill(0, 0, 0, 7);
            Stroke(0, 0, 0, 10);
            Rect(0, 0, Width, Height);

            // Game over box

            Rect(3, 4, Width - 3, Height - 2);
            Fill(255, 255, 255);
            Stroke(255, 255, 255);
            TextSize(50);
            Text("Game Over", Width / 2, 6);
            TextSize(20);
            Text("Press escape to try again", Width / 2, Height / 2);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameOver = !gameOver;
                StartGame();
            }
        }
    }

    void StartGame()
    {
        myPlayer = new Player(Width / 2, Height / 2, 0.5f);
        myBalls = new Ball[10];

        myPlayer.acceleration = new Vector2(12, 12);
        myPlayer.deceleration = new Vector2(10, 10);

        for (int i = 0; i < myBalls.Length; i++)
        {
            myBalls[i] = new Ball(Random.Range(3f, 5f), Random.Range(3f, 5f), Random.Range(0.2f, 0.5f));
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


    void Wrap(Player player)
    {
        if (player.position.x >= Width)
        {
            player.position.x = player.position.x - Width;  
        }

        if (player.position.x <= 0)
        {
            player.position.x = player.position.x + Width;
        }
    }

    void Bounce(Player player)
    {
        if (player.position.y + player.radius >= Height)
        {
            player.velocity.y *= -0.5f;
            player.position.y = Height - myPlayer.radius;
        }

        if (player.position.y - player.radius < 0)
        {
            player.velocity.y *= -0.5f;
            player.position.y = 0 + myPlayer.radius;
            
            Debug.Log(player.velocity);    
        }
    }
}