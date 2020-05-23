using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
 public float speed = 5f;
  public float jumpSpeed = 8f;
  private float movement = 0f;

  private float currentSpeedX;
  private float faceDirectionX;
   private float moveDirectionY;
	private float moveDirectionX;
  	private float jumpSpeedY;
	private float jumpUpGravity;
	private float jumpDownGravity;
    private float normalGravity;
    private bool isGrounded;
    private bool isCrouching;
	private bool isFalling;
	private bool isJumping;
    private bool isDying;
    private bool jumpButtonHeld;
	private bool jumpButtonReleased;
    
  private Rigidbody2D rigidBody;
  private Animator animator;
  private LevelManager levelManager;
  private Transform groundCheck1, groundCheck2;
  public LayerMask GroundLayers;
  	private GameController gameController;
  // Use this for initialization
  void Start () {
    rigidBody = GetComponent<Rigidbody2D> ();
    levelManager = FindObjectOfType<LevelManager>();
    animator = GetComponent<Animator> ();
    		gameController = FindObjectOfType<GameController>();
    		groundCheck1 = transform.Find ("Ground Check 1");
		groundCheck2 = transform.Find ("Ground Check 2");
    normalGravity = rigidBody.gravityScale;
    			jumpSpeedY = 19f;
			jumpUpGravity = .67f;
			jumpDownGravity = 1.64f;
            jumpButtonReleased = true;
            UpdateSize();
  }
  
  // Update is called once per frame
  void Update () {
    faceDirectionX = 1;  //Input.GetAxisRaw ("Horizontal");
    moveDirectionY = Input.GetAxisRaw ("Vertical");
    if (moveDirectionY < 0) {
        isCrouching = true;
    }else
    {
isCrouching = false;
    }
    isGrounded = Physics2D.OverlapPoint (groundCheck1.position, GroundLayers) || Physics2D.OverlapPoint (groundCheck2.position, GroundLayers);
    isFalling = rigidBody.velocity.y < 0 && !isGrounded;
    jumpButtonHeld = Input.GetButton ("Jump");
			if (Input.GetButtonUp ("Jump")) {
				jumpButtonReleased = true;
			}
            if (Input.GetButtonUp ("Fire1")) {
        levelManager.MarioPowerUp();
            }
  }
  public void UpdateSize() {
		GetComponent<Animator>().SetInteger("marioSize", FindObjectOfType<LevelManager>().marioSize);
	}
  void FixedUpdate () {

    if (faceDirectionX > 0) {
        transform.localScale = new Vector2 (1, 1); 
      rigidBody.velocity = new Vector2 (faceDirectionX * speed, rigidBody.velocity.y);
    }
    else if (faceDirectionX < 0) {
      rigidBody.velocity = new Vector2 (faceDirectionX * speed, rigidBody.velocity.y);
      transform.localScale = new Vector2 (-1, 1);
    } 
    else {
      rigidBody.velocity = new Vector2 (0,rigidBody.velocity.y);
    }

    

if (isGrounded) {
			isJumping = false;
			rigidBody.gravityScale = normalGravity;
		}
    if (!isJumping) {
			if (isGrounded && jumpButtonHeld && jumpButtonReleased) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpSpeedY);
				isJumping = true;
				jumpButtonReleased = false;
			}
		} else {  // lower gravity if Jump button held; increased gravity if released
			if (rigidBody.velocity.y > 0 && jumpButtonHeld) {
				rigidBody.gravityScale = normalGravity * jumpUpGravity;
			} else {
				rigidBody.gravityScale = normalGravity * jumpDownGravity;
			}
		}
    animator.SetFloat ("absSpeed", Mathf.Abs (faceDirectionX));
    animator.SetBool ("isJumping", isJumping);
    animator.SetBool ("isCrouching", isCrouching);

  }

  public void FreezeAndDie() {
		FreezeUserInput ();
		isDying = true;
		rigidBody.bodyType = RigidbodyType2D.Kinematic;
		animator.SetTrigger ("respawn");
        gameController.MarioControl();
	}

  public void FreezeUserInput() {
		jumpButtonHeld = false;
		jumpButtonReleased = true;
		faceDirectionX = 0;
		moveDirectionX = 0;
		currentSpeedX = 0;
		isCrouching = false;
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero; // stop all momentum
		Debug.Log (this.name + " FreezeUserInput called");
	}
    void OnCollisionEnter2D(Collision2D other) {
		Vector2 normal = other.contacts[0].normal;
		Vector2 bottomSide = new Vector2 (0f, 1f);
		bool bottomHit = normal == bottomSide;

		if (other.gameObject.tag.Contains ("Enemy") && !bottomHit) { 
			FreezeAndDie();
		
		} 
        else if (other.gameObject.tag.Contains ("Enemy") && bottomHit) { 
            Enemy enemy = other.gameObject.GetComponent<Enemy> ();
			gameController.MarioStompEnemy(enemy);
		
		} 
	}
    public Vector3 GetPosition()
    {
        return transform.position;
    }
  
}
