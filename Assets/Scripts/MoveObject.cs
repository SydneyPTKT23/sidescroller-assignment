using UnityEngine;

namespace SLC.Sidescroller
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] private float speed = 30.0f;

        private MovementController m_movementController;

        private void Start()
        {
            m_movementController = FindObjectOfType<MovementController>();
        }

        private void Update()
        {
            if (m_movementController.gameOver == false)
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left);
            }  
            
            if (transform.position.x < -15.0f && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}