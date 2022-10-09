using System.Diagnostics;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector2 = UnityEngine.Vector2;

class Emifor : IRandomWalker
{
    //Add your own variables here.
    private Vector2 _walkerPosition;
    private Vector2 _previousWalkerPosition;
    private float _width;
    private float _height;
    private float _buffer;

    private int _counter;
    private int[] _fibonacci = {4, 1, 4 , 2, 4, 2, 3, 2, 3, 2, 3, 2, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1,};

    public string GetName()
    {
        return "Emil"; //When asked, tell them our walkers name
    }

    public Vector2 GetStartPosition(int playAreaWidth, int playAreaHeight)
    {
        _width = playAreaWidth;
        _height = playAreaHeight;

        //Select a starting position or use a random one.
        float x = _width / 2;
        float y = _height / 3;

        _walkerPosition = new Vector2(x, y);
        _previousWalkerPosition = new Vector2(x, y);

        return new Vector2(x, y);
    }
    
    
    // TODO
    // First fibonacci = 4, 1, 4
    // Second fibonacci = 2, 4, 2
    // Third fibonacci = 3, 2, 3, 2, 3, 2
    // Fourth fibonacci = 3, 1, 3, 1, 3, 1, 3, 1, 3, 1
    
    
    
    public Vector2 Movement()
    {
        _counter %= _fibonacci.Length;
        switch (_fibonacci[_counter])
        {
            case 1:
                _counter++;        
                return TurnLeft();

            case 2:
                _counter++;
                return TurnRight();

            case 3:
                _counter++;
                return GoUp();

            case 4:
                _counter++;
                return GoDown();
            
            default:
                
                return new Vector2(0, 0);
        }

    }

    private Vector2 TurnLeft()
    {
        _walkerPosition.x -= 1;
        return new Vector2(-1, 0);
    }
    
    private Vector2 TurnRight()
    {
        _walkerPosition.x += 1;
        return new Vector2(1, 0);
    }

    private Vector2 GoUp()
    {
        _walkerPosition.y += 1;
        return new Vector2(0, 1);
    }

    private Vector2 GoDown()
    {
        _walkerPosition.y -= 1;
        return new Vector2(0, -1);
    }

    int DirectionSwitch()
    {
        if (_walkerPosition.y - _previousWalkerPosition.y < 0)
        {
            _previousWalkerPosition = _walkerPosition;
            return 1;
        }
        
        _previousWalkerPosition = _walkerPosition;
        return 4;
    }

   
    
} 