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
        }

        private void Update()
        {
            if (m_characterController)
            {
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
                // Death event handled here, SFX and animations.
                gameOver = true;
                m_audioSource.PlayOneShot(crashSound, 0.35f);

                m_explosionParticle.Play();
                m_runningParticle.Stop();

                m_animator.SetBool("Death_b", true);
                m_animator.SetInteger("DeathType_int", 1);
            }
        }

        private void HandleBounce()
        {
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
            // If player is grounded, do related stuff, otherwise apply gravity.
            if (m_characterController.isGrounded)
            {
                if (!m_runningParticle.isPlaying)
                    m_runningParticle.Play();

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
            // Necessary so the jump actually applies movement.
            m_characterController.Move(m_movementVector * Time.deltaTime);
        }
    }
}