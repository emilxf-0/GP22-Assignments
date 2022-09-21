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

    public bool travelRight = true;
    public bool travelDown = false;

    ParabolicCurves myParabolicCurve = new(new Vector2(9,5), new Vector2(9,10), 20);
    ParabolicCurves myParabolicCurve2 = new(new Vector2(9,10), new Vector2(5,5), 20);
    

    public class ParabolicCurves
    {
        public Vector2 axis1;
        public Vector2 axis2;
        public int numberOfLines;

        // Constructor
        public ParabolicCurves(Vector2 newAxis1, Vector2 newAxis2, int nol)
        {
            axis1 = newAxis1;
            axis2 = newAxis2;
            numberOfLines = nol;
        }
    }

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

        // Draw Parabolic curves

        StrokeWeight(0.3f);
        ParabolicCurve(myParabolicCurve.axis1, myParabolicCurve.axis2, myParabolicCurve.numberOfLines);
        ParabolicCurve(myParabolicCurve2.axis1, myParabolicCurve2.axis2, myParabolicCurve2.numberOfLines);

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

        //if (scanlines)
        //{
        //    for (int i = 0; i < Height / spaceBetweenLines; i++)
        //    {

        //        //Increase y-cord each time loop run
        //        float y = i * spaceBetweenLines;

        //        //Draw a line from left side of screen to the right
        //        Line(0, y, Width, y);
        //    }
        //    scanlines = false;
        //}
        //else
        //{
        //    for (int i = 0; i < Height / spaceBetweenLines; i++)
        //    {
        //        //Increase y-cord each time loop run
        //        float y = i * spaceBetweenLines + spaceBetweenLines;

        //        //Draw a line from left side of screen to the right
        //        Line(0, y, Width, y);
        //    }
        //    scanlines = true;
        //} 

        //if (travelRight)
        //{
        //    myParabolicCurve.axis2.x++;
        //}

        //if (myParabolicCurve.axis2.x == myParabolicCurve.axis1.x)
        //{
        //    travelRight = false;
        //}

        //if (myParabolicCurve.axis2.x <= 0)
        //{
        //    travelRight = true;
        //}

        //if (!travelRight)
        //{
        //    myParabolicCurve.axis2.x--;
        //}

        //if (myParabolicCurve.axis2.y >= Height && myParabolicCurve.axis2.x <= 0)
        //{
        //    travelDown = true; 
        //}

        //if (travelDown)
        //{
        //    myParabolicCurve.axis2.y--;
        //}

        //if (myParabolicCurve.numberOfLines < 5)
        //{
        //    myParabolicCurve.numberOfLines++;
        //}
        //if (myParabolicCurve.numberOfLines > 100)
        //{
        //    myParabolicCurve.numberOfLines--;
        //}


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
 
    void ParabolicCurve(Vector2 axis1, Vector2 axis2, int nol)
    {
        //float distanceBetweenLines = axis2.y / nol; 
        //float adjustX = axis1.x;
        //float adjustY = axis2.y;
        //float adjustX2 = axis2.x;
        //float adjustY2 = axis1.y;

        Line(axis1.x, axis1.y, axis2.x, axis2.y); //Writes out the first line

        Vector2 lineVectorAxis2 = new(axis1.x - axis2.x, axis1.y - axis2.y);
        lineVectorAxis2 = lineVectorAxis2 / nol;

        Debug.Log(lineVectorAxis2);

        for (int i = 0; i < nol; i++)
        {
            Stroke(255, 255, 255);

            axis1.x = axis1.x - lineVectorAxis2.y; //Adjusts the position of axis 1
            axis2 = axis2 + lineVectorAxis2; //Adjusts the position of axis 2

            //axis1.x = i * distanceBetweenLines + adjustX;
            //axis2.y = adjustY - (nol / nol * i * distanceBetweenLines) + adjustY2;

            if (i % 3 == 0)
            {
                Stroke(Random.Range(1, 255), 0, 0);
            }

            Line(axis1.x, axis1.y, axis2.x, axis2.y);
        }
        

        //if (myParabolicCurve.axis2.x >= Width)
        //{
        //    myParabolicCurve.axis2.x--;
        //    travelRight = false;
        //}

        //if (myParabolicCurve.axis2.x == Height)
        //myParabolicCurve.axis2.x++;
        //myParabolicCurve.axis1.y++;
    }
}
