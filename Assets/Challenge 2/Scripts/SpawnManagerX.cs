using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs; // Assign all ball prefabs in the inspector

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 22;
    private float spawnPosY = 30;

    private float startDelay = 1.0f;
    private float minSpawnInterval = 3.0f;
    private float maxSpawnInterval = 5.0f;
    private float currentSpawnInterval;

    [SerializeField] private float difficultyIncreaseRate = 0.1f; // How much to reduce interval per second
    private float minDifficultyInterval = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        StartCoroutine(SpawnBallsWithDynamicInterval());
    }

    // Coroutine to spawn balls at dynamic intervals
    private IEnumerator SpawnBallsWithDynamicInterval()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            SpawnRandomBall();

            // Adjust the spawn interval to increase difficulty over time
            currentSpawnInterval = Mathf.Max(
                Random.Range(minSpawnInterval, maxSpawnInterval) - (Time.timeSinceLevelLoad * difficultyIncreaseRate),
                minDifficultyInterval
            );

            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    // Spawn random ball at a random x position at the top of the play area
    private void SpawnRandomBall()
    {
        // Generate random ball index and random spawn position
        int randomBallIndex = Random.Range(0, ballPrefabs.Length); // Randomize ball type
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // Instantiate ball at random spawn location
        Instantiate(ballPrefabs[randomBallIndex], spawnPos, ballPrefabs[randomBallIndex].transform.rotation);
    }
}