using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //We don't need to multiply with Time.deltaTime since it's already the right unit.
        Vector2 movement = new Vector2(x, y) * speed;

        rb2d.velocity = movement;
        
        //get mouse postion pixel vector
        Vector2 mousePos = Input.mousePosition;

        //Use the camera to convert pixel postion to world position
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //Set player up direction to the relative direction of the mouse
        transform.up = (Vector3)mousePos - transform.position;
    }
    
}