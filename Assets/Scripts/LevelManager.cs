using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int marioSize;
    private float transformDuration = 1;
    public bool gamePaused;
	private Mario mario;

    	private Animator mario_Animator;
	private Rigidbody2D mario_Rigidbody2D;
	public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        marioSize =0 ;
        gamePaused = false;
        mario = FindObjectOfType<Mario> ();
		mario_Animator = mario.gameObject.GetComponent<Animator> ();
		mario_Rigidbody2D = mario.gameObject.GetComponent<Rigidbody2D> ();
		mario.UpdateSize ();
        // InvokeRepeating ("SpawnEnemy", 0.20f, 0.3f);

    }
    public void MarioPowerUp() {
		if (marioSize < 2) {
			StartCoroutine (MarioPowerUpCo ());
		}
	}
	IEnumerator MarioPowerUpCo() {
		mario_Animator.SetBool ("isPoweringUp", true);
		Time.timeScale = 0f;
		mario_Animator.updateMode = AnimatorUpdateMode.UnscaledTime;

		yield return new WaitForSecondsRealtime (transformDuration);
		yield return new WaitWhile(() => gamePaused);

		Time.timeScale = 1;
		mario_Animator.updateMode = AnimatorUpdateMode.Normal;
		marioSize++;
		mario.UpdateSize ();
		mario_Animator.SetBool ("isPoweringUp", false);
	}
    // Update is called once per frame
    void Update()
    {
			
	    }
	public void ExecuteFunction(){

	}
	public void SpawnEnemy(){
		Instantiate(enemy, new Vector3(Random.Range(-10,10), 6, 0), Quaternion.identity);
	}
}
