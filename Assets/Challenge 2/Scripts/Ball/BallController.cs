using Scripts.Managers;
using UnityEngine;

namespace Scripts.Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallType _ballType;
        [SerializeField] private float speed = 2f;
        [SerializeField] private Animator animator; // Reference to the Animator component

        private bool isBeingDestroyed = false; // To prevent multiple destruction triggers

        public BallType Type => _ballType;
        private void Update()
        {
            if (!isBeingDestroyed)
            {
                MoveBall();
                
                if (transform.position.y < -1f)
                {
                    GameManager.Instance.PlayBallDestroySound();
                    Destroy(gameObject);
                }
            }
        }

        private void MoveBall()
        {
            switch (_ballType)
            {
                case BallType.PowerUp:
                    // Default falling behavior
                    transform.Translate(Vector3.down * speed * 8 * Time.deltaTime);
                    break;
                case BallType.Red:
                    // Default falling behavior
                    transform.Translate(Vector3.forward * speed * 10 * Time.deltaTime);
                    break;
                case BallType.Blue:
                    // Moves faster than the others
                    transform.Translate(Vector3.forward * (speed * 10 * 1.5f) * Time.deltaTime);
                    break;
                case BallType.Green:
                    // Changes direction mid-fall
                    Vector3 direction = Mathf.Sin(Time.time * 2f) > 0 ? Vector3.left : Vector3.right;
                    transform.Translate((Vector3.forward + direction * 0.5f) * speed * 10 * Time.deltaTime);
                    break;
            }
        }

        public void DestroyBall()
        {
            if (isBeingDestroyed) return;
            GetComponent<Collider>().enabled = false;
            isBeingDestroyed = true;

            // Trigger destruction animation
            animator.SetTrigger("destroy");

            // Destroy the ball after the animation finishes
            Destroy(gameObject, 0.25f); // Adjust delay based on the animation length
        }
    }

    public enum BallType
    {
        Red,
        Blue,
        Green,
        PowerUp
    }
}
