using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ballPrefabs;
    
    [SerializeField] private float difficultyIncreaseRate = 0.1f;

    [SerializeField] private float spawnLimitXLeft = -22;
    [SerializeField] private float spawnLimitXRight = 22;
    private float _spawnPosY = 30;

    private float _startDelay = 1.0f;
    private float _minSpawnInterval = 3.0f;
    private float _maxSpawnInterval = 5.0f;
    private float _currentSpawnInterval;

    private float _minDifficultyInterval = 1.0f;

    private void Start()
    {
        _currentSpawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        StartCoroutine(SpawnBallsWithDynamicInterval());
    }
    
    private IEnumerator SpawnBallsWithDynamicInterval()
    {
        yield return new WaitForSeconds(_startDelay);

        while (true)
        {
            SpawnRandomBall();
            
            _currentSpawnInterval = Mathf.Max(
                Random.Range(_minSpawnInterval, _maxSpawnInterval) - (Time.timeSinceLevelLoad * difficultyIncreaseRate),
                _minDifficultyInterval
            );

            yield return new WaitForSeconds(_currentSpawnInterval);
        }
    }
    
    private void SpawnRandomBall()
    {
        int randomBallIndex = Random.Range(0, ballPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), _spawnPosY, 0);

        GameObject ball = Instantiate(ballPrefabs[randomBallIndex], spawnPos, ballPrefabs[randomBallIndex].transform.rotation);
        
        // Destroy the ball after 10 seconds if it doesn't get collected
        Destroy(ball, 10);
    }
}