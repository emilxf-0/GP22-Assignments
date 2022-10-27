using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector2 = UnityEngine.Vector2;

class Emifor : IRandomWalker
{
    //Add your own variables here.
    private enum DirectionState
    {
        DownLeft,
        DownRight,
        UpRight,
        UpLeft,
    }
    
    private Vector2 walkerPosition;
    private Vector2 previousWalkerPosition;
    private float width;
    private float height;
    private float buffer = 10;

    private int counter;
    private int[] fibonacciDownLeft = {4, 1, 4};
    private int[] fibonacciDownRight = {2, 4, 2};
    private int[] fibonacciUpRight = {3, 2, 3};
    private int[] fibonacciUpLeft = {1, 3, 1};

    private int[] fibonacci = {1, 1, 3, 5, 8, 13, 21};
    private int[] coordinates;
    private int coordinate;
    private int currentFibonacci = 1;
    private int currentFibonacciIndex = 1;
    private int step;
    private bool goingForward = true;

    private DirectionState mirrorState;
    private DirectionState currentState;
    private DirectionState nextState;
    
    
    public string GetName()
    {
        return "Emil"; //When asked, tell them our walkers name
    }
    
    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
    {
        width = playAreaWidth;
        height = playAreaHeight;

        //Select a starting position or use a random one.
        float x = width / 2;
        float y = height / 3;

        walkerPosition = new Vector2(x, y);
        previousWalkerPosition = new Vector2(x, y);
        
        //Creates an array containing a fibonacci sequence
        // var n = 20;
        // for (int i = 0; i < n; i++)
        // {
        //     fibonacci = new int[n];
        //     fibonacci[i] = FibonacciSequence(i);
        //     Debug.Log(fibonacci[i]);
        // }
        
        //Sets the start fibonacci number to one to prevent illegal move of 0
        currentFibonacciIndex = 1;
        currentFibonacci = 1;
        
        // Sets the start direction to down left
        currentState = DirectionState.DownLeft;

        return new Vector2(x, y);
    }

    public Vector2 Movement()
    {

        if (currentState == DirectionState.DownLeft)
        {
            mirrorState = DirectionState.UpRight;
            nextState = DirectionState.DownRight;
            coordinates = fibonacciDownLeft;
        }
        else if (currentState == DirectionState.DownRight)
        {
            mirrorState = DirectionState.UpLeft;
            nextState = DirectionState.UpRight;
            coordinates = fibonacciDownRight;
        }
        else if (currentState == DirectionState.UpRight)
        {
            mirrorState = DirectionState.DownLeft;
            nextState = DirectionState.UpLeft;
            coordinates = fibonacciUpRight;
        }
        else 
        {
            mirrorState = DirectionState.DownRight;
            nextState = DirectionState.DownLeft;
            coordinates = fibonacciUpLeft;
        }

        coordinate = counter % coordinates.Length;
        Debug.Log("Step " + step);
        
        
        Debug.Log("Current fib " + currentFibonacci);
        Debug.Log("Index" + currentFibonacciIndex);

        if (counter % 3 == 0)
        {
            step++;
        }

        if (OutOfBounds(width, height, buffer))
        {
            Array.Reverse(fibonacci);
            currentFibonacciIndex = 0;
            goingForward = !goingForward;
        }

        if (step == currentFibonacci)
        {
            step = 0;
            currentFibonacciIndex++;

            if (currentFibonacciIndex > fibonacci.Length)
            {
                currentFibonacciIndex = 0;
            }
            
            currentFibonacci = fibonacci[currentFibonacciIndex];

            if (goingForward)
            {
                currentState = nextState;
            }
            else
            {
                currentState = mirrorState;
            }
        }
        
        switch (coordinates[coordinate])
            {
                case 1:
                    counter++;
                    return TurnLeft();

                case 2:
                    counter++;
                    return TurnRight();

                case 3:
                    counter++;
                    return GoUp();

                case 4:
                    counter++;
                    return GoDown();
                
                default:
                    return new Vector2(0,0);
            }
    }

    private Vector2 TurnLeft()
    {
        walkerPosition.x -= 1;
        return new Vector2(-1, 0);
    }
    
    private Vector2 TurnRight()
    {
        walkerPosition.x += 1;
        return new Vector2(1, 0);
    }

    private Vector2 GoUp()
    {
        walkerPosition.y += 1;
        return new Vector2(0, 1);
    }

    private Vector2 GoDown()
    {
        walkerPosition.y -= 1;
        return new Vector2(0, -1);
    }

    bool OutOfBounds(float width, float height, float buffer)
    {
        width -= buffer;
        height -= buffer;
        if (walkerPosition.x >= width || walkerPosition.x <= 0 + buffer)
        {
            return true;
        }
        else if (walkerPosition.y >= height || walkerPosition.y <= 0 + buffer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int DirectionSwitch()
    {
        if (walkerPosition.y - previousWalkerPosition.y < 0)
        {
            previousWalkerPosition = walkerPosition;
            return 1;
        }
        
        previousWalkerPosition = walkerPosition;
        return 4;
    }

    int FibonacciSequence(int number)
    {
        if (number == 0)
            return 0;
        else if (number == 1)
            return 1;
        else 
            return FibonacciSequence(number - 1) + FibonacciSequence(number - 2);
    }

} 