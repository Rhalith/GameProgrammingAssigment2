using UnityEngine;

namespace Scripts.Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallType _ballType;
        [SerializeField] private float speed = 2f;

        public BallType Type => _ballType;

        private void Update()
        {
            MoveBall();

            // Destroy the ball if it falls out of bounds
            if (transform.position.y < -6f)
            {
                Destroy(gameObject);
            }
        }

        private void MoveBall()
        {
            switch (_ballType)
            {
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
    }

    public enum BallType
    {
        Red,
        Blue,
        Green
    }
}