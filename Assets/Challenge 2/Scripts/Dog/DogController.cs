using System;
using Scripts.Managers;
using UnityEngine;

namespace Scripts.Dog
{
    public class DogController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision detected" + other.gameObject.name);
            if (other.gameObject.CompareTag("Ball"))
            {
                Destroy(other.gameObject);
                int points = other.gameObject.GetComponent<Ball.BallController>().Type switch
                {
                    Ball.BallType.Red => 1,
                    Ball.BallType.Blue => 2,
                    Ball.BallType.Green => 3,
                };
                GameManager.Instance.ScoreManager.AddScore(points);
            }
        }
    }
}