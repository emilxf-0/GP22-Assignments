using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOver : ProcessingLite.GP21
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        Background(125, 45, 100);
        Fill(255, 255, 255);
        Stroke(255, 255, 255);
        TextSize(50);
        Text("Game Over", Width / 2, Height / 2);
        TextSize(30);
        Text("Press escape to try again", Width / 2, Height / 2 - 2);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<GameOver>().enabled = false;
            GetComponent<Movement>().enabled = true;
        }
    }
}
