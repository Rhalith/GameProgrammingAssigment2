using System.Collections;
using System.Collections.Generic;
using Scripts.Managers;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] private GameObject dogPrefab;
    [SerializeField] private float spawnCooldown = 1.0f;
    
    private float _lastSpawnTime = 0.0f;
    private void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _lastSpawnTime + spawnCooldown)
        {
            SpawnDog();
        }
    }

    private void SpawnDog()
    {
        Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        _lastSpawnTime = Time.time;
    }
}
