using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public int Type;
    public List<Transform> itemsToSpawn;
    public List<Transform> bricksToSpawn;
    public bool enableSpawn = true;
    private void Awake()
    {
        if (enableSpawn)
        {
            Transform chosenItem = itemsToSpawn[Random.Range(0, itemsToSpawn.Count)];
            Vector2 spawncompute = new Vector2(Random.Range(gameObject.transform.Find("StartPosition").position.x, gameObject.transform.Find("EndPosition").position.x), Random.Range(0, -3.4f));
            Instantiate(chosenItem, spawncompute, Quaternion.identity);

            chosenItem = bricksToSpawn[Random.Range(0, bricksToSpawn.Count)];
            spawncompute = new Vector2(Random.Range(gameObject.transform.Find("StartPosition").position.x - 0.5f, gameObject.transform.Find("EndPosition").position.x), Random.Range(-0.8f, 1.5f));
            Instantiate(chosenItem, spawncompute, Quaternion.identity);
        }

    }
}
