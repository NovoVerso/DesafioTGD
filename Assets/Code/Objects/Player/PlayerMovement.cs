using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    private int fruits;
    private bool gameOver = false;
    private GameObject enemy;
    private enum MovementState {idle, running, jumping, falling };
    [SerializeField] public GameObject FloatingText;

    [SerializeField] private float moveSpeed = 7f, jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    private GameObject text;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        fruits = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (fruits == 12 && gameOver == false)
        {
            gameOver = true;
            text = Instantiate(FloatingText);
        }
        dirX = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(dirX * moveSpeed, body.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }

        UpdateAnimation();



    }
    private void UpdateAnimation()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (body.velocity.y > 0f)
        {
            state = MovementState.jumping;
        }
        else if (body.velocity.y < 0f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.gameObject;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("SampleScene");
        }

        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            fruits++;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            BoxCollider2D enemyColl = enemy.GetComponent<BoxCollider2D>();
            if (enemyColl.size.y / 2 + coll.size.y / 2 <= coll.bounds.center.y-enemyColl.bounds.center.y)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce/2);
                Debug.Log("tô por cima");
   
                collision.gameObject.GetComponent<Woodies>().hit();
            }

            Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        }
    }
}



