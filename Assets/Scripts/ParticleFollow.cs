using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    //private ParticleSystem particleSystem; // Reference to the ParticleSystem component

    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        //particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of the particle system to follow the player
            transform.position = new Vector3(playerTransform.position.x, 11, 0);
        }
    }
}
