using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ProcessingLite.GP21
{
    Vector2 circle1;
    Vector2 circle2;

    public float speed = 5f;
    public float acceleration = 3f;
    public float deceleration = 3f;
    public float accelerationSpeed;
    public bool decelerate = false;

    private float axisValueX;
    private float axisValueY;

    float diameter = 2;
    float radius;

    bool wrapRight;
    bool wrapLeft;

    public Vector2 axis;

    void Start()
    {
        accelerationSpeed = 0;
        radius = diameter / 2;
        // Startposition first circle
        circle1.x = Width / 2; //middle of the screen
        circle1.y = Height / 2;

        // Startposition second circle
        circle2.x = circle1.x; 
        circle2.y = circle1.y + diameter * 1.5f; // Keeps the distance even if we fiddle with circle size
    }

    void Update()
    {
        Background(50, 166, 240);
        
        // Puts a cap on speed
        accelerationSpeed = Mathf.Clamp(accelerationSpeed, 0, 7);

        if (decelerate)
        {
            accelerationSpeed -= deceleration * Time.deltaTime;
            circle2 += axis * accelerationSpeed * Time.deltaTime;
    
            if (accelerationSpeed <= 0)
            {
                accelerationSpeed = 0;
                decelerate = false;
            }
        }
        
        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            decelerate = true;
        }

        // Accelerates Circle2
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            decelerate = false;

            accelerationSpeed += acceleration * Time.deltaTime;

            axisValueX = Input.GetAxisRaw("Horizontal");
            axisValueY = Input.GetAxisRaw("Vertical");
            axis = new Vector2(axisValueX, axisValueY).normalized;
        }

        Wrap(circle1);
        Wrap(circle2);
        
        // Circle1
        circle1.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        circle1.y += Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        Circle(circle1.x, circle1.y, diameter);

        // Circle2
        circle2.x += Input.GetAxisRaw("Horizontal") * accelerationSpeed * Time.deltaTime;
        circle2.y += Input.GetAxisRaw("Vertical") * accelerationSpeed * Time.deltaTime;
        Circle(circle2.x, circle2.y, diameter);

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
            Circle(vector.x - Width, vector.y, diameter);
        }

        if (wrapLeft)
        {
            Circle(vector.x - Width, vector.y, diameter);
        }

        if (vector.y + radius >= Height)
        {
            vector.y = Height - radius;
        }

        if (vector.y - radius < 0)
        {
            vector.y = 0 + radius;
        }
    }
}
