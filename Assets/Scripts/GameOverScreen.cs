using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLC.Sidescroller
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject m_panel;
        private MovementController m_movementController;

        private void Start()
        {
            // Set panel to false on startup just in case.
            m_panel.SetActive(false);
            m_movementController = FindObjectOfType<MovementController>();
        }

        private void Update()
        {
            if (m_movementController.gameOver)
                m_panel.SetActive(true);
        }

        // Function to reset the scene, tied to a button OnClick event.
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}