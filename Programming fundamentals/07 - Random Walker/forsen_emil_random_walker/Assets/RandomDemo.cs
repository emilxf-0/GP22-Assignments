using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDemo : ProcessingLite.GP21
{
    int timeStep = 0;   //So we can keep track of time

    //Keeping track of previous values so we can draw lines.
    float previousRandomValue = 0;
    float previousNoiseValue = 0;
    float previousCustomValue = 0;

    int[] gaussianNumbers;
    int gaussianSample = 200;

    void Start()
    {
        //Prepare our scene with all the following settings
        Application.targetFrameRate = 20;
        StrokeWeight(0.5f);
        Background(0);
        timeStep = 0;
        gaussianNumbers = new int[gaussianSample];
    }

    void Update()
    {
        //Restart the program on mouse click
        if (Input.GetMouseButtonDown(0))
            Start();

        //Loop the program 100 times then pause
        if (timeStep > 100)
            return;

        //Draw the following graphs
        //Comment out to draw one at a time.
        DrawRandomGraph();          //White graph, Normal randomization
        DrawPerlinNoiseGraph();	 //Green graph, perlin noise.
        DrawCustomGraph();          //Gold graph, a random graph that changes over time
        DrawNormalizedGraph();       //light gray dots, normalized distribution.

        //Move one tick/frame/step
        timeStep++;
    }

    ///Draws a graph with completely random numbers.
    void DrawRandomGraph()
    {
        Stroke(255);

        //"Normal" random value between zero and screen height.
        float y = Random.Range(0, Height);

        //Calculate X positions that we use for visualizations
        float oldX = (timeStep - 1) * Width / 100;
        float x = timeStep * Width / 100; //100 data points distributed over the screen

        //Draw a line from the previous point to the new point
        Line(oldX, previousRandomValue, x, y);

        //Save the old point (we don't need X, it can be calculated)
        previousRandomValue = y;
    }
    
    void DrawPerlinNoiseGraph()
    {
        Stroke(0, 255, 0);

        //Noise will give a value 0->1, multiply with height.
        //We give the function the current "time" and we will get values that affected by its neighbors.
        float y = Mathf.PerlinNoise(timeStep * 0.05f, 0) * Height;

        float oldX = (timeStep - 1) * Width / 100;
        float x = timeStep * Width / 100;

        Line(oldX, previousNoiseValue, x, y);

        previousNoiseValue = y;
    }
    
    void DrawCustomGraph()
    {
        Stroke(200, 200, 0);

        float y;

        //We can influence the random result by adding rules (formulas)
        y = Random.Range(0, (timeStep * 0.33f % Height));

        //Or we can use the previous value like this
        //y = Random.Range(0, Height / 2 + Height % (previousCustomValue + 1));

        float oldX = (timeStep - 1) * Width / 100;
        float x = timeStep * Width / 100;

        Line(oldX, previousCustomValue, x, y);

        previousCustomValue = y;
    }
    
    void DrawNormalizedGraph()
    {
        Stroke(200);

        //A normalized graph has to be done in a different way
        //Number closer to the middle should be more common.
        //We randomize 200 times each frame and summarize the results.
        for (int i = 0; i < gaussianSample; ++i)
        {
            //randomGaussian has no max or min value but its normalized
            int index;

            //Magic calculation for gaussian normalization found online.
            float u1 = 1.0f - Random.Range(0, 1f); //uniform(0,1) random floats
            float u2 = 1.0f - Random.Range(0, 1f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                                  Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
            float randNormal = Width * 7 + 20 * randStdNormal; //random normal(mean,stdDev^2)

            //Make sure that we are in range of the index or otherwise skip it.
            index = Mathf.RoundToInt(randNormal);
            if (index > gaussianSample-1 || index < 0)
                continue;

            //put each number found in the right part of the array
            gaussianNumbers[index]++;
        }

        //Draw the array.
        for (int i = 0; i < gaussianSample; ++i)
        {
            Point(i / Width, gaussianNumbers[i] / Height / 7);
        }
    }


}