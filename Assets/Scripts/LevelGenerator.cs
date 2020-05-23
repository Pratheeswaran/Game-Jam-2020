/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Mario player;

    private Vector3 lastEndPosition;

    private void Awake() {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 2;
        for (int i = 0; i < startingSpawnLevelParts; i++) {
            SpawnLevelPart();
        }
    }

    private void Update() {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) {
            // Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        Vector3 spawncompute = spawnPosition;
        if (levelPart.name == "Ground-1")
        {
            spawncompute[1] = -4.55f;
            spawncompute[0] = spawnPosition[0] + 2f;
        }
        else if (levelPart.name == "Ground-2")
        {
            spawncompute[1] = -5.15f;
            spawncompute[0] = spawnPosition[0] + 3.5f;
        }
        else if (levelPart.name == "Ground-3")
        {
            spawncompute[1] = -3.43f;
            spawncompute[0] = spawnPosition[0] + 3f;
        }
        Transform levelPartTransform = Instantiate(levelPart, spawncompute, Quaternion.identity);
        return levelPartTransform;
    }

}
