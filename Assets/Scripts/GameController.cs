using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
  public GameObject FloatingTextEffect;
    public int score;
    public int coin;
    private Mario mario;
    public Vector2 stompBounceVelocity = new Vector2 (0, 15);
    void Awake() {
      if (FindObjectsOfType (GetType ()).Length == 1) {
            DontDestroyOnLoad (gameObject);
          } else {
            Destroy (gameObject);
          }

	  }
 
    void Start()
    {
        score =0;
        coin = 0;
        mario = FindObjectOfType<Mario> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int to_add,Vector2 spawnPos)
    {
      score += to_add;
      CreateFloatingText (score.ToString (), spawnPos);
    }
    public void MarioStompEnemy(Enemy enemy) {
      mario.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (mario.gameObject.GetComponent<Rigidbody2D> ().velocity.x + stompBounceVelocity.x, stompBounceVelocity.y);
      enemy.StompedByMario ();
      // soundSource.PlayOneShot (stompSound);
      AddScore (100, enemy.gameObject.transform.position);

	  }
    public void MarioControl()
    {
      // Destroy(mario.gameObject);
      LoadEnd(1);
    }
public void CreateFloatingText(string text, Vector3 spawnPos) {
		GameObject textEffect = Instantiate (FloatingTextEffect, spawnPos, Quaternion.identity);
		textEffect.GetComponentInChildren<TextMesh> ().text = text.ToUpper ();
	}
    IEnumerator LoadSceneDelayCo(string sceneName, float delay = 0) {
		yield return new WaitForSecondsRealtime (delay);
		SceneManager.LoadScene (sceneName);
	}
    public void LoadStart(float delay = 0) {
		StartCoroutine (LoadSceneDelayCo ("Startl", delay));
	}
    public void LoadEnd(float delay = 0) {
		StartCoroutine (LoadSceneDelayCo ("End", delay));
	}
    public void LoadLevel(float delay = 0) {
		StartCoroutine (LoadSceneDelayCo ("Level", delay));
	}
}
