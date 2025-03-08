using UnityEngine;

public class WolverineCollision : MonoBehaviour
{
    public Animator helaAnimator; // Assign Hela's Animator in Inspector
    public Camera helaCamera; // Assign Hela's Camera in Inspector
    public Camera mainCamera; // Assign Main Camera in Inspector
    public string triggerTag = "Trigger"; // Tag for the camera trigger area
    public string helaTag = "Hela"; // Tag for Hela (which is a trigger)

    public string hitOrMiss;

    void Start()
    {
        // Ensure Hela Camera is disabled at the start
        if (helaCamera != null)
            helaCamera.gameObject.SetActive(false);
        hitOrMiss = "MISS";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag)) // If Wolverine enters the camera trigger area
        {
            Debug.Log("Wolverine entered the trigger area!");

            // Enable Hela Camera
            if (helaCamera != null)
            {
                helaCamera.gameObject.SetActive(true);
            }

            // Disable Main Camera (optional)
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
            }

            // Disable Hela Camera after 5 seconds
            Invoke("DisableHelaCamera", 3f);
        }

        if (other.CompareTag(helaTag)) // If Wolverine enters Hela's trigger
        {
            hitOrMiss = "HIT";
            Debug.Log("Wolverine hit Hela!");
            Debug.Log(hitOrMiss);

            

            if (helaAnimator != null)
            {
                helaAnimator.SetBool("Hit", true); // Play Hela's animation
                Debug.Log("Hela's animation triggered!");
            }
            else
            {
                Debug.LogError("Hela Animator is not assigned!");
            }
        }
    }

    void DisableHelaCamera()
    {
        if (helaCamera != null)
        {
            helaCamera.gameObject.SetActive(false);
            Debug.Log("Hela's camera disabled after 5 seconds.");
        }

        // Re-enable main camera (optional)
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
    }
}
