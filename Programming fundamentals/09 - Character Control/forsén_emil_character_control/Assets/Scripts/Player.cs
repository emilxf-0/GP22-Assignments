using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float _charger;

    private UIManager _uiManager;
    private SpriteRenderer _spriteRenderer;
    private int _score;

    private bool _isTouchingGround;
    
    private Vector2 _movement;

    

    Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isTouchingGround = false;
        speed = 5;
        jumpForce = 5;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_isTouchingGround)
        {
            if (Input.GetButton("Jump"))
            {
                _charger += Time.deltaTime * 2f;
                _charger = Mathf.Clamp(_charger, 0, 3f);
                _uiManager.ChargeJump(_charger);
            }

            if (Input.GetButtonUp("Jump"))
            {
                jumpForce += _charger;
                
                Jump();
                _charger = 0;
                jumpForce = 5;
                _uiManager.ChargeJump(_charger);
            }
            
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            _spriteRenderer.flipX = false;
        }
        
        _movement.x = Input.GetAxis("Horizontal") * speed;
        _movement.y = rb2d.velocity.y;
        rb2d.velocity = _movement;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isTouchingGround = true;
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    private void Jump()
    {
        rb2d.velocity = new Vector2(0, jumpForce);
        _isTouchingGround = !_isTouchingGround;
    }
    

}
