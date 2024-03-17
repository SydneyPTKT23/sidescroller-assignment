using UnityEngine;

namespace SLC.Sidescroller
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private float startDelay = 2.0f;
        [SerializeField] private float repeatRate = 2.0f;
        [Space]
        [SerializeField] private GameObject prefab;

        private MovementController m_movementController;
        private Vector3 spawnPosition = new(25, 0, 0);

        private void Start()
        {
            // Invoke a repeating of the obstacle spawn function with set times.
            m_movementController = FindObjectOfType<MovementController>();
            InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        }

        private void SpawnObstacle()
        {
            if (m_movementController.gameOver == false)
            {
                Instantiate(prefab, spawnPosition, prefab.transform.rotation);
            }
        }
    }
}