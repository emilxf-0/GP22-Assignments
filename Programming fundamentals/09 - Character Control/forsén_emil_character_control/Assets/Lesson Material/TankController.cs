using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float turningSpeed = 50;
    public float speed = 15;

    private float _angle;
    private Rigidbody2D _rb2d;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _angle -= Input.GetAxis("Horizontal") * turningSpeed *Time.deltaTime;
        _rb2d.MoveRotation(_angle);

        float y = Input.GetAxis("Vertical");
        _rb2d.velocity = _rb2d.transform.up * y * speed;
    }
}
