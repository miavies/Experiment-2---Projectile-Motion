using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerIndicator : MonoBehaviour
{
    public RectTransform arrowTransform;
    public float maxScale = 2.0f;
    public float chargeSpeed = 2.0f;
    public float launchForceMultiplier = 10.0f;
    public Rigidbody wolverineRb;
    public Transform hulkHand;
    public Animator hulkAnimator;

    public Camera mainCamera;
    public Camera wolverineCamera;

    public GameObject uiCanvas; // UI Container (Assign in Inspector)

    private Vector2 originalSize;
    private bool isCharging = false;
    private float power = 0f;

    void Start()
    {
        originalSize = arrowTransform.sizeDelta;

        // Ensure UI starts hidden
        if (uiCanvas != null) uiCanvas.SetActive(false);

        // Enable UI after 1 second
        Invoke("EnableUI", 1.5f);
    }

    void Update()
    {
        RotateIndicator();
        HandlePowerCharge();
    }

    void RotateIndicator()
    {
        Vector2 direction = (Input.mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void HandlePowerCharge()
    {
        if (Input.GetMouseButton(0)) // Charging
        {
            isCharging = true;
            power = Mathf.Min(power + chargeSpeed * Time.deltaTime, maxScale);
            arrowTransform.sizeDelta = new Vector2(originalSize.x * (1 + power), arrowTransform.sizeDelta.y);
        }
        else if (isCharging) // Release to Launch
        {
            isCharging = false;
            LaunchWolverine();
            power = 0;
            arrowTransform.sizeDelta = originalSize;
        }
    }

    void LaunchWolverine()
    {
        if (wolverineRb != null)
        {
            wolverineRb.transform.SetParent(null);
            wolverineRb.isKinematic = false;
            wolverineRb.useGravity = true;

            Vector2 launchDirection = (Input.mousePosition - transform.position).normalized;
            wolverineRb.linearVelocity = launchDirection * (power * launchForceMultiplier);

            // Switch to Wolverine's Camera
            SwitchCamera(true);

            // Trigger the Throw animation
            if (hulkAnimator != null)
            {
                hulkAnimator.SetBool("Throw", true);
            }

            Debug.Log("Wolverine Launched with Camera Switch!");

            // Disable UI after launch
            if (uiCanvas != null) uiCanvas.SetActive(false);
        }
    }

    void SwitchCamera(bool toWolverineCamera)
    {
        if (mainCamera != null && wolverineCamera != null)
        {
            mainCamera.gameObject.SetActive(!toWolverineCamera);
            wolverineCamera.gameObject.SetActive(toWolverineCamera);

            mainCamera.enabled = !toWolverineCamera;
            wolverineCamera.enabled = toWolverineCamera;
        }
    }

    void EnableUI()
    {
        if (uiCanvas != null)
            uiCanvas.SetActive(true);
    }
}
