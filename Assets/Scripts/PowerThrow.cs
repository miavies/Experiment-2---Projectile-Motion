using UnityEngine;
using UnityEngine.UI;

public class PowerIndicator : MonoBehaviour
{
    public RectTransform arrowTransform;
    public float maxScale = 2.0f;
    public float chargeSpeed = 2.0f;
    public float launchForceMultiplier = 10.0f; // Adjust this value for strength
    public Rigidbody wolverineRb; // Reference to Wolverine's Rigidbody
    public Transform hulkHand; // Reference to Hulk's hand (parent object)

    private Vector2 originalSize;
    private bool isCharging = false;
    private float power = 0f;

    void Start()
    {
        originalSize = arrowTransform.sizeDelta;
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
            wolverineRb.transform.SetParent(null); // Detach from Hulk's hand
            wolverineRb.isKinematic = false; 

            Vector2 launchDirection = (Input.mousePosition - transform.position).normalized;
            wolverineRb.linearVelocity = launchDirection * (power * launchForceMultiplier);

            Debug.Log("Wolverine Launched!");
        }
    }
}
