using UnityEngine;

public class WolverineCollision : MonoBehaviour
{
    public Animator helaAnimator;
    public Camera helaCamera;
    public Camera mainCamera;
    public string triggerTag = "Trigger";
    public string helaTag = "Hela";

    public GameObject Killfeed;

    [Header("Audio Settings")]
    public AudioSource audioSource; // Drag an AudioSource component here
    public AudioClip launchSound;   // Assign Wolverine launch sound
    public AudioClip hitSound;      // Assign Hela hit sound

    public string hitOrMiss;

    void Start()
    {
        if (helaCamera != null)
            helaCamera.gameObject.SetActive(false);

        if (Killfeed != null)
            Killfeed.SetActive(false);

        hitOrMiss = "MISS";
    }

    public void PlayLaunchSound()
    {
        if (audioSource != null && launchSound != null)
        {
            audioSource.PlayOneShot(launchSound);
            Debug.Log("Wolverine launch sound played!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            Debug.Log("Wolverine entered the trigger area!");

            if (helaCamera != null)
                helaCamera.gameObject.SetActive(true);

            if (mainCamera != null)
                mainCamera.gameObject.SetActive(false);

            Invoke("DisableHelaCamera", 3f);
        }

        if (other.CompareTag(helaTag))
        {
            hitOrMiss = "HIT";
            Debug.Log("Wolverine hit Hela!");
            Debug.Log(hitOrMiss);

            if (helaAnimator != null)
            {
                helaAnimator.SetBool("Hit", true);
                Debug.Log("Hela's animation triggered!");
            }

            if (Killfeed != null)
                Killfeed.SetActive(true);

            PlayHitSound();
        }
    }

    void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            Debug.Log("Hela hit sound played!");
        }
    }

    void DisableHelaCamera()
    {
        if (helaCamera != null)
        {
            helaCamera.gameObject.SetActive(false);
            Debug.Log("Hela's camera disabled after 3 seconds.");
        }

        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
    }
}
