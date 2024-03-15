using UnityEngine;

namespace SLC.Sidescroller
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float jumpForce = 10.0f;

        [Header("Ground Settings")]
        [SerializeField] private float gravityMultiplier = 1.5f;
        [SerializeField] private float stickToGroundForce = 2.0f;

        [Header("DEBUG")]
        [SerializeField] private Vector3 m_movementVector;

        private CharacterController m_characterController;

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (m_characterController)
            {
                AddDownForce();
                AddMovement();
            }
        }

        private void HandleBounce()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_movementVector.y = jumpForce;
            }
        }

        private void AddDownForce()
        {
            if (m_characterController.isGrounded)
            {
                m_movementVector.y = -stickToGroundForce;
                HandleBounce();
            }
            else
            {
                m_movementVector += gravityMultiplier * Time.deltaTime * Physics.gravity;
            }
        }

        private void AddMovement()
        {
            m_characterController.Move(m_movementVector * Time.deltaTime);
        }
    }
}