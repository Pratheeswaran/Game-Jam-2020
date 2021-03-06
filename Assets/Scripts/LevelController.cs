﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    // Level Generate vaiables
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 15f;

    public Transform levelPart_Start;
    public List<Transform> levelPartList;
    public List<Transform> levelPartListHard;
    public Mario player;

    private Vector3 lastEndPosition;

    private int difficulty = 0;
    public AudioSource mainTheme;
    public AudioSource deadMario;
    public AudioSource hurryMario;
    public AudioSource jumpMario;
    public AudioSource stompMario;

    public Text scoreText;
    public Text coinText;
    public Text meterText;

    public GameController gameController;

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 1;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnGround();
        }
    }

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        mainTheme.Play();
    }
    void Update()
    {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnGround();
        }
        SetHudCoin();
        SetHudScore();
        SetHudMeter();
    }

    private void FixedUpdate()
    {
        ManageDiffuculty();
    }

    Color currentColor = new Color(138 / 225f, 168 / 255f, 223 / 255f);
    bool camColorChanged = false;
    bool camColorChangedInit = false;

    IEnumerator LerpColor()
    {
        float progress = 0;
        float increment = 0.02f / 3.5f;
        camColorChangedInit = true;
        while (progress < 1)
        {
            Camera.main.backgroundColor = Color.Lerp(currentColor, Color.black, progress);
            progress += increment;
            yield return new WaitForSeconds(0.02f);
        }
        camColorChanged = true;
        mainTheme.Stop();
        hurryMario.Play();
    }

    public void ManageDiffuculty()
    {
        if (gameController.meter > 100 )
        {
            difficulty = 1;
            if (!camColorChanged && !camColorChangedInit)
                StartCoroutine("LerpColor");
        }
        else if (gameController.meter > 150)
        {
            difficulty = 2;
        }
        else if (gameController.meter > 200)
        {
            difficulty = 3;
        }
    }
    public int GetDiffuculty()
    {
        return difficulty;
    }

    public void SetHudCoin()
    {
        coinText.text = "x" + gameController.coin.ToString("D2");
    }

    public void SetHudScore()
    {
        scoreText.text = gameController.score.ToString("D6");
    }
    public void SetHudMeter()
    {
        meterText.text = gameController.meter.ToString("D3");
    }

    private void SpawnGround()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        if (difficulty > 0)
        {
            chosenLevelPart = levelPartListHard[Random.Range(0, levelPartListHard.Count)];
        }
        Transform lastLevelPartTransform = SpawnGround(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnGround(Transform levelPart, Vector3 spawnPosition)
    {
        Vector3 spawncompute = spawnPosition;
        switch (levelPart.name[7])
        {
            case '1':
                spawncompute.y = -4.55f;
                spawncompute.x = spawnPosition.x + 3.5f;
                break;
            case '2':
                spawncompute.y = -5.15f;
                spawncompute.x = spawnPosition.x + 3.5f;
                break;
            case '3':
                spawncompute.y = -3.43f;
                spawncompute.x = spawnPosition.x + 3.5f;
                break;
            case '4':
                spawncompute.y = -3.43f;
                spawncompute.x = spawnPosition.x + 6f;
                break;
            case '5':
                spawncompute.y = -5.18f;
                spawncompute.x = spawnPosition.x + 3.5f;
                break;
            default:
                spawncompute.y = -5.18f;
                spawncompute.x = spawnPosition.x + 3.5f;
                break;
        }

        Transform levelPartTransform = Instantiate(levelPart, spawncompute, Quaternion.identity);
        return levelPartTransform;
    }
}
