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
        [Space]
        [SerializeField] private LayerMask groundLayer = ~0;
        [Range(0f, 1f)] [SerializeField] private float rayLength = 0.1f;
        [Range(0.01f, 1f)] [SerializeField] private float raySphereRadius = 0.1f;

        [Header("DEBUG")]
        [SerializeField] private Vector3 m_movementVector;
        [SerializeField] private float m_finalRayLength;
        [SerializeField] private bool m_isGrounded;

        public ParticleSystem m_explosionParticle;
        public ParticleSystem m_runningParticle;

        public AudioClip crashSound;
        public AudioClip jumpSound;

        public bool gameOver = false;

        private AudioSource m_audioSource;
        private CharacterController m_characterController;
        private Animator m_animator;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_characterController = GetComponent<CharacterController>();
            m_animator = GetComponentInChildren<Animator>();

            m_finalRayLength = rayLength + m_characterController.center.y;
        }

        private void Update()
        {
            if (m_characterController)
            {
                CheckIfGrounded();

                AddDownForce();
                AddMovement();
            }
        }

        private void OnCollisionEnter(Collision t_collision)
        {
            // The t_ -naming convention is a personal distinction I like to make
            // for temporary variables.            
            if (t_collision.gameObject.CompareTag("Obstacle"))
            {
                gameOver = true;
                m_audioSource.PlayOneShot(crashSound, 0.35f);

                m_explosionParticle.Play();
                m_runningParticle.Stop();

                m_animator.SetBool("Death_b", true);
                m_animator.SetInteger("DeathType_int", 1);
            }
        }

        private void CheckIfGrounded()
        {
            Vector3 t_origin = transform.position + m_characterController.center;
            bool t_hitGround = Physics.SphereCast(t_origin, raySphereRadius, Vector3.down, out _, m_finalRayLength, groundLayer);
            Debug.DrawRay(t_origin, Vector3.down * (m_finalRayLength), Color.red);

            m_isGrounded = t_hitGround;
        }

        private void HandleBounce()
        {
            if (m_isGrounded && !gameOver)
            {
                if (!m_runningParticle.isPlaying)
                    m_runningParticle.Play();
            }

            if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
            {
                m_audioSource.PlayOneShot(jumpSound, 0.35f);
                if(m_runningParticle.isPlaying)
                    m_runningParticle.Stop();

                m_animator.SetTrigger("Jump_trig");
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