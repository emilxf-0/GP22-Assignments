using UnityEngine;

public class WalkerTest : ProcessingLite.GP21
{
    //This file is only for testing your movement/behavior.
    //The Walkers will compete in a different program!

    IRandomWalker walker;
    Vector2 walkerPos;
    float scaleFactor = 0.05f;

    void Start()
    {
        Background(125, 110, 97);
        //Some adjustments to make testing easier
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;

        //Create a walker from the class Example it has the type of WalkerInterface
        walker = new Emifor();

        //Get the start position for our walker.
        walkerPos = walker.GetStartPosition((int)(Width / scaleFactor), (int)(Height / scaleFactor));
    }

    void Update()
    {
        //Draw the walker
        Point(walkerPos.x * scaleFactor, walkerPos.y * scaleFactor);
        //Get the new movement from the walker.
        walkerPos += walker.Movement();

        if (Input.GetMouseButtonDown(0))
            Start();
        
    }
}