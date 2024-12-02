using System;
using System.Collections;
using Scripts.Managers;
using UnityEngine;

namespace Scripts.Dog
{
    public class DogController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float speed = 30f;
        private bool _isBarking;

        private void Update()
        {
            if (!_isBarking)
            {
                MoveForward();
            }
            if(transform.position.x < -30)
            {
                Destroy(gameObject);
            }
        }

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                _isBarking = true;
                
                Destroy(other.gameObject);
                
                int points = other.gameObject.GetComponent<Ball.BallController>().Type switch
                {
                    Ball.BallType.Red => 1,
                    Ball.BallType.Blue => 2,
                    Ball.BallType.Green => 3,
                    _ => 0
                };
                
                GameManager.Instance.ScoreManager.AddScore(points);
                
                if (animator != null)
                {
                    animator.SetTrigger("bark");
                }

                StartCoroutine(ResumeMovementAfterBark());
            }
        }

        private IEnumerator ResumeMovementAfterBark()
        {
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Bark"))
            {
                yield return null;
            }
            
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
            
            yield return new WaitForSeconds(animationTime);
            
            _isBarking = false;
        }
    }
}
