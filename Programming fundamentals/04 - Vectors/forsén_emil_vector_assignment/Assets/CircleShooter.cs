using Mono.Cecil;
using ProcessingLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShooter : ProcessingLite.GP21
{
    public Vector2 circlePosition;
    public Vector2 mousePosition;
    public float diameter = 0.2f;
    bool launched = false;
    float speed;

    Vector2 lineVector; 

    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {

        // Draws a new circle every frame
        Background(155, 30, 155);
        Circle(circlePosition.x, circlePosition.y, diameter);

        if (launched)
        {
            circlePosition = circlePosition + (lineVector * Time.deltaTime * Mathf.Clamp(speed, 0, 1.5f));
        }

        // Change direction when hitting a wall
        if (circlePosition.x <= 0 || circlePosition.x >= Width)  
        {
            lineVector.x *= -1;
        }

        if (circlePosition.y <= 0 || circlePosition.y >= Height)
        {
            lineVector.y *= -1;
        }

        // Teleport the circle
        if (Input.GetMouseButtonDown(0))
        {
            launched = false;
            circlePosition.x = MouseX;
            circlePosition.y = MouseY;            
        }

        // Draw a line between the circle and mouse
        if (Input.GetMouseButton(0))
        {
            Line(circlePosition.x, circlePosition.y, MouseX, MouseY);
        }

        // Give the circle direction
        if (Input.GetMouseButtonUp(0))
        {
            mousePosition = new(MouseX, MouseY);
            lineVector = new(MouseX - circlePosition.x, MouseY - circlePosition.y);
            speed = Magnitude(lineVector);
            launched = true;
        }  

    }
    public float Magnitude(Vector2 vector)
    {
        float sqrtLength = (vector.x * vector.x) + (vector.y * vector.y);
        float length = Mathf.Sqrt(sqrtLength);
        return length;
    }
}
