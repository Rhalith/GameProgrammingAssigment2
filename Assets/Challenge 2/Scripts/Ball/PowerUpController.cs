using UnityEngine;

namespace Scripts.Ball
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private Material objectMaterial;
        [SerializeField] private float alphaChangeSpeed = 2f;
        
        private bool _increasingAlpha = true;

        private void Start()
        {
            alphaChangeSpeed *= 100f;
            if (objectMaterial == null)
            {
                objectMaterial = GetComponent<Renderer>().material;
            }
        }

        private void Update()
        {
            UpdateAlpha();
        }

        private void UpdateAlpha()
        {
            Color currentColor = objectMaterial.color;
            
            if (_increasingAlpha)
            {
                currentColor.a += alphaChangeSpeed * Time.deltaTime / 255f;

                if (currentColor.a >= 1f)
                {
                    currentColor.a = 1f;
                    _increasingAlpha = false;
                }
            }
            else
            {
                currentColor.a -= alphaChangeSpeed * Time.deltaTime / 255f;

                if (currentColor.a <= 100f / 255f)
                {
                    currentColor.a = 100f / 255f;
                    _increasingAlpha = true;
                }
            }
            objectMaterial.color = currentColor;
        }
    }
}