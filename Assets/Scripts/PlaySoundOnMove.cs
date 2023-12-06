using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public float movementThreshold = 0.01f; // Minimum movement to trigger sound

    private Vector3 lastPosition; // Last frame's position

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if the GameObject has moved
        if (Vector3.Distance(transform.position, lastPosition) > movementThreshold && !audioSource.isPlaying)
        {
            audioSource.Play(); // Play sound
        }

        lastPosition = transform.position; // Update last position for the next frame
    }
}