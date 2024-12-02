using System.Collections;
using Scripts.Managers;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] private GameObject dogPrefab;
    [SerializeField] private float spawnCooldown = 0.3f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xLimit = 19f;
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 25f;

    private float _lastSpawnTime;
    private bool _isFacingRight;
    private Quaternion _targetRotation;

    private void Start()
    {
        _targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _lastSpawnTime + spawnCooldown)
        {
            SpawnDog();
        }

        MovePlayer();
        SmoothlyRotatePlayer();
    }

    private void SpawnDog()
    {
        GameManager.Instance.PlayDogSpawnSound();

        // Determine spawn offset and rotation
        Vector3 spawnOffset = _isFacingRight ? Vector3.right : Vector3.left;
        Vector3 spawnPosition = transform.position + spawnOffset * 1.2f;
        Quaternion spawnRotation = _isFacingRight ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);

        // Instantiate the dog
        Instantiate(dogPrefab, spawnPosition, spawnRotation);

        _lastSpawnTime = Time.time;
    }


    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        Vector3 movement = new Vector3(horizontal, 0, 0) * moveSpeed * Time.deltaTime;
        
        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
        transform.position = newPosition;
        
        animator.SetFloat("Speed_f", Mathf.Abs(horizontal));
        
        if (horizontal > 0 && !_isFacingRight)
        {
            FlipPlayer(true);
        }
        else if (horizontal < 0 && _isFacingRight)
        {
            FlipPlayer(false);
        }
    }

    private void FlipPlayer(bool facingRight)
    {
        _isFacingRight = facingRight;
        
        float targetRotationY = _isFacingRight ? 90 : -90;
        _targetRotation = Quaternion.Euler(0, targetRotationY, 0);
    }

    private void SmoothlyRotatePlayer()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
    }
}
