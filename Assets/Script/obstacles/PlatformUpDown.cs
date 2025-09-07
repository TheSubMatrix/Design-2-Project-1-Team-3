using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    public float speed = 2f; // Speed of the movement
    public float height = 5f; // Height of the movement

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;

        // Update the object's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
