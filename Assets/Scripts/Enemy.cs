using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public bool canMove = false;
    public bool canMoveAutomatic = false;
    private float minDistanceToMove = 8f;

    private float stompedDuration = 0.5f;
    public bool isBeingStomped;
    public Vector2 flippedVelocity = new Vector2(0, 3);
    public float directionX = -1;
    public Vector2 Speed = new Vector2(3, 0);
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody2D;
    private GameObject mario;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        mario = FindObjectOfType<Mario>().gameObject;
        OrientSprite();
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (!canMove & Mathf.Abs(mario.transform.position.x - transform.position.x) <= minDistanceToMove)
        {
            canMove = true;
        }
    }
    public void StompedByMario()
    {
        isBeingStomped = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
        m_Animator.SetTrigger("stomped");
        Destroy(gameObject, stompedDuration);
        isBeingStomped = false;
    }
    void OrientSprite()
    {
        if (directionX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (directionX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            m_Rigidbody2D.velocity = new Vector2(Speed.x * directionX, m_Rigidbody2D.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normal = other.contacts[0].normal;
        Vector2 leftSide = new Vector2(-1f, 0f);
        Vector2 rightSide = new Vector2(1f, 0f);
        bool sideHit = normal == leftSide || normal == rightSide;
        if (other.gameObject.tag != "Player" && sideHit)
        {
            directionX = -directionX;
            OrientSprite();
        }

    }

    protected virtual void FlipAndDie()
    {
        Animator m_Animator = GetComponent<Animator>();
        Rigidbody2D m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator.SetTrigger("flipped");
        m_Rigidbody2D.velocity += flippedVelocity;
    }
}
