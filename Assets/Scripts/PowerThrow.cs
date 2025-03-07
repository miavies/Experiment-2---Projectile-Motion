using UnityEngine;
using UnityEngine.UI;

public class PowerIndicator : MonoBehaviour
{
    public RectTransform arrowTransform; // Reference to the UI arrow
    public float maxScale = 2.0f; // Fixed maximum scale value
    public float chargeSpeed = 2.0f; // Speed of scaling
    private Vector2 originalSize;
    private bool isCharging = false;

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
        if (Input.GetMouseButton(0)) // Holding left mouse button
        {
            isCharging = true;
            float scaleFactor = Mathf.Min(arrowTransform.sizeDelta.x + chargeSpeed * Time.deltaTime * 100, originalSize.x * maxScale);
            arrowTransform.sizeDelta = new Vector2(scaleFactor, arrowTransform.sizeDelta.y);
        }
        else if (isCharging)
        {
            isCharging = false;
            LaunchProjectile();
            arrowTransform.sizeDelta = originalSize; // Reset size
        }
    }

    void LaunchProjectile()
    {
        // Implement Wolverine launch logic here
        Debug.Log("Wolverine Launched!");
    }
}
