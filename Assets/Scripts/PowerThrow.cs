using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PowerIndicator : MonoBehaviour
{
    public Image arrowCharge;
    public float maxScale = 2.0f;
    public float chargeSpeed = 2.0f;
    public float launchForceMultiplier = 10.0f;
    public float angle;
    public Rigidbody wolverineRb;
    public Transform hulkHand;
    public Animator hulkAnimator;
    public TrailRenderer wolverineTrail;

    public Camera mainCamera;
    public Camera wolverineCamera;
    public Camera valuesCamera;

    public GameObject uiCanvas; // UI Container (Assign in Inspector)

    private bool isCharging = false;
    public float power = 0f;

    public TextMeshProUGUI angleText;

    void Start()
    {
        arrowCharge.fillAmount = 0;
        wolverineTrail.enabled = false;

        if (uiCanvas != null)
            uiCanvas.SetActive(false);

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
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        angleText.text = angle.ToString("F1") + "°";
    }

    void HandlePowerCharge()
    {
        if (Input.GetMouseButton(0)) // Charging
        {
            isCharging = true;
            power = Mathf.Min(power + chargeSpeed * Time.deltaTime, maxScale);
            arrowCharge.fillAmount = power / maxScale;
        }
        else if (isCharging) // Release to Launch
        {
            isCharging = false;
            LaunchWolverine();
            power = 0;
            arrowCharge.fillAmount = 0;
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

            SwitchCamera(wolverineCamera);

            if (hulkAnimator != null)
            {
                hulkAnimator.SetBool("Throw", true);
            }

            wolverineTrail.enabled = true;

            Debug.Log("Wolverine Launched with Camera Switch!");

            if (uiCanvas != null)
                uiCanvas.SetActive(false);

            // Switch to Values Camera after 10 seconds
            Invoke("SwitchToValuesCamera", 10f);
        }
    }
   
    void SwitchCamera(Camera activeCamera)
    {
        mainCamera.gameObject.SetActive(activeCamera == mainCamera);
        wolverineCamera.gameObject.SetActive(activeCamera == wolverineCamera);
        valuesCamera.gameObject.SetActive(activeCamera == valuesCamera);
    }

    void SwitchToValuesCamera()
    {
        SwitchCamera(valuesCamera);
    }

    void EnableUI()
    {
        if (uiCanvas != null)
            uiCanvas.SetActive(true);
    }

}
