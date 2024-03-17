using UnityEngine;

namespace SLC.Sidescroller
{
    public class RepeatBackground : MonoBehaviour
    {
        private Vector3 startPosition;
        private float repeatWidth;

        private void Start()
        {
            startPosition = transform.position;
            repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        }

        private void Update()
        {
            // If position gets too close to the edge, reset it.
            if (transform.position.x < startPosition.x - repeatWidth)
            {
                transform.position = startPosition;
            }
        }
    }
}