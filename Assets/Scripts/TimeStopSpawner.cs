using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopSpawner : MonoBehaviour
{
    public GameObject timeStopCollectiblePrefab;
    public float spawnInterval = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCollectible", spawnInterval, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCollectible()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(14, 40), Random.Range(-3, 4));
        Instantiate(timeStopCollectiblePrefab, spawnPosition, Quaternion.identity);
    }
}
