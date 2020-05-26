using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public int Type;
    public List<Transform> itemsToSpawn;
    public List<Transform> bricksToSpawn;
    public List<Transform> itemsToSpawnH;
    public List<Transform> bricksToSpawnH;
    public bool enableSpawn = true;
    private void Awake()
    {
        if (enableSpawn)
        {
            //Hard
            if (this.name.Contains("-H"))
            {
                Spawn(itemsToSpawnH, bricksToSpawnH);
            }
            //Normal
            else
            {
                Spawn(itemsToSpawn, bricksToSpawn);
            }

            
        }


    }
    void Spawn(List<Transform> itemsForLevel, List<Transform> bricksForLevel)
    {
        Transform chosenItem = itemsForLevel[Random.Range(0, itemsForLevel.Count)];
        Vector2 spawncompute = new Vector2(Random.Range(gameObject.transform.Find("StartPosition").position.x, gameObject.transform.Find("EndPosition").position.x), Random.Range(0, -3.4f));
        Instantiate(chosenItem, spawncompute, Quaternion.identity);
        Debug.Log(this.name);
        if (!this.name.Contains("Ground-5"))
        {
            chosenItem = bricksForLevel[Random.Range(0, bricksForLevel.Count)];
            spawncompute = new Vector2(Random.Range(gameObject.transform.Find("StartPosition").position.x - 0.5f, gameObject.transform.Find("EndPosition").position.x), Random.Range(-0.8f, 1.5f));
            Instantiate(chosenItem, spawncompute, Quaternion.identity);
        }
    }
}
