using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ProcessingLite.GP21
{
    Vector2 positionCircle1;
    Vector2 positionCircle2;

    float diameter = 2;
    public float speed = 5f;
    public float acceleration = 3f;
    public float deceleration = 3f;
    public float accelerationSpeed;
    public bool decelerate = false;

    private float axisValueX;
    private float axisValueY;

    
    public Vector2 axis;

    void Start()
    {
        accelerationSpeed = 0;

        // Startposition first circle
        positionCircle1.x = Width / 2; //middle of the screen
        positionCircle1.y = Height / 2;

        // Startposition second circle
        positionCircle2.x = positionCircle1.x; 
        positionCircle2.y = positionCircle1.y + diameter * 1.5f; // Keeps the distance even if we fiddle with circle size
    }

    void Update()
    {
        Background(50, 166, 240);
        
        // Puts a cap on speed
        accelerationSpeed = Mathf.Clamp(accelerationSpeed, 0, 7);

        if (decelerate)
        {
            accelerationSpeed -= deceleration * Time.deltaTime;
            positionCircle2 += axis * accelerationSpeed * Time.deltaTime;
            
    
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

        
        // Circle1
        positionCircle1.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        positionCircle1.y += Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        Circle(positionCircle1.x, positionCircle1.y, diameter);

        // Circle2
        positionCircle2.x += Input.GetAxisRaw("Horizontal") * accelerationSpeed * Time.deltaTime;
        positionCircle2.y += Input.GetAxisRaw("Vertical") * accelerationSpeed * Time.deltaTime;
        Circle(positionCircle2.x, positionCircle2.y, diameter);

    }

}
