using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject FloatingTextEffect;
    public int score;
    public int coin;
    public int meter;
    private Mario mario;
    public Vector2 stompBounceVelocity = new Vector2(0, 15);
    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        NewGame();
    }

    void NewGame()
    {
        meter = 0;
        score = 0;
        coin = 0;
    }

    void Update()
    {
        mario = FindObjectOfType<Mario>();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void AddScore(int to_add, Vector2 spawnPos)
    {
        score += to_add;
        CreateFloatingText(to_add.ToString(), spawnPos);
    }
    public void AddCoin(int to_add, Vector2 spawnPos)
    {
        coin += to_add;
        CreateFloatingText(to_add.ToString(), spawnPos);
    }
    public void SetMeter(int new_meter)
    {
        meter = new_meter;
    }
    public void MarioStompEnemy(Enemy enemy)
    {

        mario.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(mario.gameObject.GetComponent<Rigidbody2D>().velocity.x + stompBounceVelocity.x, stompBounceVelocity.y);
        enemy.StompedByMario();
        AddScore(100, enemy.gameObject.transform.position);

    }
    public void GameOver()
    {
        LoadEnd(4); // Music to end
    }
    public void CreateFloatingText(string text, Vector3 spawnPos)
    {
        GameObject textEffect = Instantiate(FloatingTextEffect, spawnPos, Quaternion.identity);
        textEffect.GetComponentInChildren<TextMesh>().text = text.ToUpper();
    }
    IEnumerator LoadSceneDelayCo(string sceneName, float delay = 0)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }
    public void LoadEnd(float delay = 0)
    {
        StartCoroutine(LoadSceneDelayCo("End", delay));
    }
    public void LoadLevel(float delay = 0)
    {
        NewGame();
        StartCoroutine(LoadSceneDelayCo("Level", delay));
    }
}
