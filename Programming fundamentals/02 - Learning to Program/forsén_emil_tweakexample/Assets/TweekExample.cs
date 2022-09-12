using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TweekExample : MonoBehaviour
{
    //public GameObject laserPrefab;
    public float x = 2;
    public float y = 3;
    public float angle = 4;
    public float size = 1;

    public float fireRate = 0.2f;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && timer > fireRate)
        {
            // Instantiate(laserPrefab, transform.position, transform.rotation);
            GameObject newLaser = new GameObject("Laser");
            newLaser.transform.position = transform.position;
            newLaser.transform.rotation = transform.rotation;
            newLaser.AddComponent<SpriteRenderer>();
            newLaser.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            newLaser.GetComponent<SpriteRenderer>().color = Color.red;
            newLaser.transform.localScale = new Vector3 (0.05f, 0.6f, 1);
            newLaser.AddComponent<Rigidbody2D>().velocity = newLaser.transform.up * 10;
            newLaser.GetComponent<Rigidbody2D>().gravityScale = 0;
            Destroy(newLaser, 3);

            GetComponent<AudioSource>().Play();
            

            timer = 0;
        }
        timer += Time.deltaTime;


        //Movement

        x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * 5;
        y += Input.GetAxisRaw("Vertical") * Time.deltaTime * 5;

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, Input.mousePosition, Time.deltaTime * 5);
        //}

        

        //Scale
        if (Input.GetKeyDown(KeyCode.E))
        {
            size++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            size--;
        }

        transform.position = new Vector3(x, y);
        transform.localScale = Vector3.one * size;
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Rotation
        Vector2 aim = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.up = aim;
    }
}
