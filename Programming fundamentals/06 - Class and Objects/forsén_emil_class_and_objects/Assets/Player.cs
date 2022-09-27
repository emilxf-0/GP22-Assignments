using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : ProcessingLite.GP21
{

    // Variables

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 deceleration;

    float axisValueX;
    float axisValueY;
    Vector2 axis;

    public float radius;
    public float size;

    // Constructor

    public Player(float x, float y, float size)
    {
        position = new Vector2(x, y);
        velocity = new Vector2(1, 1);
        this.size = size;
        radius = size / 2;
    }
    
    public void Draw()
    {
        Fill(175, 15, 175);
        Stroke(175, 15, 175);
        Circle(position.x, position.y, size);
    }

    public void UpdatePosition()
    {
        position.x += Input.GetAxisRaw("Horizontal") * velocity.x * Time.deltaTime;
        position.y += Input.GetAxisRaw("Vertical") * velocity.y * Time.deltaTime;

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            axisValueX = Input.GetAxisRaw("Horizontal");
            axisValueY = Input.GetAxisRaw("Vertical");
            axis = new Vector2(axisValueX, axisValueY);

            velocity += acceleration * Time.deltaTime;
        }
        else
        {
            velocity -= deceleration * Time.deltaTime;
            position +=  axis * velocity * Time.deltaTime;

            if (velocity.x <= 0 || velocity.y <= 0)
            {
                velocity *= 0;
            }
        }
    }

    public void MaxVelocity()
    {
        velocity.x = Mathf.Clamp(velocity.x, -12, 12);
        velocity.y = Mathf.Clamp(velocity.y, -12, 12);
    }

}
