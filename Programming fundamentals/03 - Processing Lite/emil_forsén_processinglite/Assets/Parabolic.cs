using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabolic : ProcessingLite.GP21
{
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

    int red = 0;
    int green = 0;
    int blue = 0;

    public ParabolicCurves myParabolicCurve = new ParabolicCurves(new Vector2(9, 0), new Vector2(9, 10), 50);
    public ParabolicCurves myParabolicCurve2 = new ParabolicCurves(new Vector2(9, 0), new Vector2(9, 10), 50);


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 10;
        Background(23, 23, 23);
    }

    // Update is called once per frame
    void Update()
    {
        // Draw Parabolic curves
        StrokeWeight(0.5f);

        ParabolicCurve(myParabolicCurve.axis1, myParabolicCurve.axis2, myParabolicCurve.numberOfLines, false);
        ParabolicCurve(myParabolicCurve2.axis1, myParabolicCurve2.axis2, myParabolicCurve2.numberOfLines, true);
    }

    void ParabolicCurve(Vector2 axis1, Vector2 axis2, int nol, bool mirrored)
    {
        Line(axis1.x, axis1.y, axis2.x, axis2.y); //Writes out the first line

        Vector2 lineVectorAxis2 = new(axis1.x - axis2.x, axis1.y - axis2.y);
        lineVectorAxis2 = lineVectorAxis2 / nol;

        red = Random.Range(25, 190);
        green = Random.Range(100, 150);
        blue = Random.Range(70, 200);

        for (int i = 0; i < nol; i++)
        {
            Stroke(red, green, blue);
            if (mirrored)
            {
                axis1.x = axis1.x + lineVectorAxis2.y; //Adjusts the position of axis 1
                axis2 = axis2 + lineVectorAxis2; //Adjusts the position of axis 2
            }

            if (!mirrored)
            {
                axis1.x = axis1.x - lineVectorAxis2.y; //Adjusts the position of axis 1
                axis2 = axis2 + lineVectorAxis2; //Adjusts the position of axis 2
            }

            Line(axis1.x, axis1.y, axis2.x, axis2.y);
        }
    }
}