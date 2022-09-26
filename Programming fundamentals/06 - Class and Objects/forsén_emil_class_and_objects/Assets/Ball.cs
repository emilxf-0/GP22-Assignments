using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Ball : ProcessingLite.GP21
{
    //Our class variables
    public Vector2 position; //Ball position
    public Vector2 velocity; //Ball direction

    public float size;
    public float radius;

    int r;
    int g;
    int b;

    public Ball(float x, float y, float size)
    {
        //Set our position when we create the code.
        position = new Vector2(x, y);
        this.size = size;
        radius = size / 2;

        //Create the velocity vector and give it a random direction.
        velocity = new Vector2();
        velocity.x = Random.Range(0, 11) - 5;
        velocity.y = Random.Range(0, 11) - 5;

        // Gives a random color to every ball
        r = Random.Range(150, 200);
        g = Random.Range(75, 100);
        b = Random.Range(10, 15);

    }

    public void Draw()
    {
        Fill(r, g, b);
        Stroke(r, g, b);
        Circle(position.x, position.y, size);
    }

    public void UpdatePos()
    {
        position += velocity * Time.deltaTime;
    }

    public void CheckCollision()
    {
        if (position.y + radius >= Height || position.y - radius <= 0)
        {
            velocity.y *= -1;
        }

        if (position.x + radius >= Width || position.x - radius <= 0)
        {
            velocity.x *= -1;
        }
    }    
}
