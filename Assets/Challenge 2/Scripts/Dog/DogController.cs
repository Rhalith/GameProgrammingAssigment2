using System;
using System.Collections;
using Scripts.Ball;
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
            if(transform.position.x < -30 || transform.position.x > 35)
            {
                Destroy(gameObject);
            }
        }

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                _isBarking = true;
                
                BallController ballController = other.gameObject.GetComponent<BallController>();
                
                ballController.DestroyBall();
                
                GameManager.Instance.PlayBallCollectSound();
                
                int points = ballController.Type switch
                {
                    BallType.Red => 1,
                    BallType.Blue => 2,
                    BallType.Green => 3,
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
        
        public void BarkSound()
        {
            GameManager.Instance.PlayDogCollectSound();
        }
    }
}
