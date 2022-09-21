using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Assignment1 : ProcessingLite.GP21
{
    float spaceBetweenLines = 0.2f;

    public Vector2 ePosition;
    public Vector2 mPosition;
    public Vector2 iPosition;
    public Vector2 lPosition;

    public bool scanlines;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // Background color
        Background(23, 23, 23);

        // Some shapes

        Fill(25, 0, 200);
        StrokeWeight(1);
        Stroke(200, 0, 25);
        Square(2, 2, 3);

        Fill(220, 0, 113);
        Stroke(25, 0, 200);
        Square(5, 5, 3);
        
        Circle(7, 7, 3);

        Stroke(25, 250, 0);
        Triangle(13, 2, 14, 4, 15, 2);

        //My name (it's Emil)
        Stroke(255, 255, 255);
        StrokeWeight(2);

        LetterE(ePosition.x, ePosition.y);
        LetterM(mPosition.x, mPosition.y);
        LetterI(iPosition.x, iPosition.y);
        LetterL(lPosition.x, lPosition.y);

        //Draw our scan lines
        Stroke(200, 0, 200, 64);
        StrokeWeight(0.5f);

    }

    void LetterE(float x, float y)
    {
        Line(x, y, x, y - 4);
        Line(x, y, x + 2, y);
        Line(x, y - 2, x + 2, y - 2);
        Line(x, y - 4, x + 2, y - 4);
    }

    void LetterM(float x, float y)
    {
        Line(x, y, x, y - 3);
        Line(x, y - 0.3f, x + 1, y - 0.3f);
        Line(x + 1, y - 0.3f, x + 1, y - 3);
        Line(x + 1, y - 0.3f, x + 2, y - 0.3f);
        Line(x + 2, y - 0.3f, x + 2, y - 3);
    }

    void LetterI(float x, float y)
    {
        Line(x, y, x, y - 2.5f);
        Line(x, y + 1.5f, x, y + 1.3f);
    }

    void LetterL(float x, float y)
    {
        Line(x, y, x, y - 4);
    }
 
    
}
