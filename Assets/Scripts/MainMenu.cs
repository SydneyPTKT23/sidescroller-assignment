using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLC.Sidescroller
{
    public class MainMenu : MonoBehaviour
    {
        public Scene gameScene;

        public void StartGame()
        {
            SceneManager.LoadScene("Prototype 3");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}