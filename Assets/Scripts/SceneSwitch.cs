using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppv;
    [SerializeField] private AudioSource birdSounds;
    [SerializeField] private AudioSource rainSounds;
    private ColorGrading cg;

    void Start()
    {
        // Ensure Post Process Volume component is assigned
        if (ppv == null)
        {
            Debug.LogError("Post Process Volume component is not assigned!");
            return;
        }

        // Get the ColorGrading effect from the Post Process Volume
        if (!ppv.profile.TryGetSettings(out cg))
        {
            Debug.LogError("ColorGrading effect not found in the Post Process Volume!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the ColorGrading effect
            if (cg != null)
            {
                cg.active = true;
                birdSounds.Pause();
                rainSounds.Play();
            }
            else
            {
                Debug.LogWarning("ColorGrading effect is not properly configured!");
            }
        }
    }
}
