using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public float initialSpeed = 5f;
    private float speed = 5f;
    public float jumpSpeed = 8f;

    private float jumpSpeedY = 19f;
    private float jumpUpGravity = .67f;
    private float jumpDownGravity = 1.64f;
    private float normalGravity;
    private bool isGrounded;
    private bool isJumping;
    private bool isDying = false;
    private bool jumpButtonHeld;
    private bool jumpButtonReleased;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private LevelController levelController;
    private Transform groundCheck1, groundCheck2;
    public LayerMask GroundLayers;
    private GameController gameController;
    private float deadUpTimer = .25f;

    void Start()
    {
         deadUpTimer = 
        speed = initialSpeed;
        rigidBody = GetComponent<Rigidbody2D>();
        levelController = FindObjectOfType<LevelController>();
        animator = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
        groundCheck1 = transform.Find("Ground Check 1");
        groundCheck2 = transform.Find("Ground Check 2");
        normalGravity = rigidBody.gravityScale;
        jumpButtonReleased = true;

    }

    void Update()
    {
        
        
        isGrounded = Physics2D.OverlapPoint(groundCheck1.position, GroundLayers) || Physics2D.OverlapPoint(groundCheck2.position, GroundLayers);
        jumpButtonHeld = Input.GetButton("Jump") || Input.GetButton("Fire1");
        if (Input.GetButton("Jump") || Input.GetButton("Fire1"))
        {
            jumpButtonReleased = true;
        }
        if (isDying)
        {
            deadUpTimer -= Time.unscaledDeltaTime;
            if (deadUpTimer > 0)
            { // TODO MovePosition not working
              //					m_Rigidbody2D.MovePosition (m_Rigidbody2D.position + deadUpVelocity * Time.unscaledDeltaTime);
                gameObject.transform.position += Vector3.up * .22f;
            }
            else
            {
                //					m_Rigidbody2D.MovePosition (m_Rigidbody2D.position + deadDownVelocity * Time.unscaledDeltaTime);
                gameObject.transform.position += Vector3.down * .2f;
            }
        }
    }
    void FixedUpdate()
    {
        if (!isDying)
        {
            // Run
            speed = initialSpeed + levelController.GetDiffuculty();
            animator.SetFloat("absSpeed", 1);
            gameController.SetMeter(((int)transform.position.x) + 5);
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        
        if (isGrounded)
        {
            isJumping = false;
            rigidBody.gravityScale = normalGravity;
        }
        if (!isJumping)
        {

            if (isGrounded && jumpButtonHeld && jumpButtonReleased)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeedY);
                levelController.jumpMario.Play();
                isJumping = true;
                jumpButtonReleased = false;
            }
        }
        else
        {  // lower gravity if Jump button held; increased gravity if released
            if (rigidBody.velocity.y > 0 && jumpButtonHeld)
            {
                rigidBody.gravityScale = normalGravity * jumpUpGravity;
            }
            else
            {
                rigidBody.gravityScale = normalGravity * jumpDownGravity;
            }
        }
        
        animator.SetBool("isJumping", isJumping);

    }

    public void FreezeAndDie()
    {
        FreezeUserInput();
        isDying = true;
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        animator.SetTrigger("respawn");
        levelController.mainTheme.Stop();
        levelController.deadMario.Play();
        gameController.GameOver();
    }

    public void FreezeUserInput()
    {
        jumpButtonHeld = false;
        jumpButtonReleased = true;
        rigidBody.velocity = Vector3.zero; // stop all momentum
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normal = other.contacts[0].normal;
        Vector2 bottomSide = new Vector2(0f, 1f);
        bool bottomHit = normal == bottomSide;

        if (other.gameObject.tag.Contains("Enemy") && !bottomHit)
        {
            FreezeAndDie();

        }
        else if (other.gameObject.tag.Contains("Enemy") && bottomHit)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            levelController.stompMario.Play();
            gameController.MarioStompEnemy(enemy);

        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
