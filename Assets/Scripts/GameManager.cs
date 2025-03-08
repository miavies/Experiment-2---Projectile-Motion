using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI initialVelocityText;
    public TextMeshProUGUI maxHeightText;
    public TextMeshProUGUI distanceTravelledText;
    public TextMeshProUGUI timeOfFlightText;
    public TextMeshProUGUI HitOrMissText;

    private PowerIndicator powerIndicator;
    public GameObject power;
    public GameObject chibiWolvie;

    private float maxHeight;
    private float distanceTravelled;
    private float timeOfFlight;

    public WolverineCollision wolverineCollision;

    private void Start()
    {
        powerIndicator = power.GetComponent<PowerIndicator>();
        wolverineCollision = chibiWolvie.GetComponent<WolverineCollision>();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        if (chibiWolvie.transform.parent != null)
        {
            //Angle
            angleText.text = powerIndicator.angleText.text;

            //Initial Velocity
            float velocityMagnitude = powerIndicator.power * powerIndicator.launchForceMultiplier;
            initialVelocityText.text = velocityMagnitude.ToString("F2") + " m/s";

            //Max Height
            maxHeight = Mathf.Pow((velocityMagnitude * Mathf.Sin(powerIndicator.angle * Mathf.Deg2Rad)), 2)/(2*9.81f);
            maxHeightText.text = maxHeight.ToString("F2") + "m";

            //Distance Travelled
            distanceTravelled = (Mathf.Pow(velocityMagnitude, 2) * Mathf.Sin(2 * powerIndicator.angle * Mathf.Deg2Rad)) / 9.81f;
            distanceTravelledText.text = distanceTravelled.ToString("F2") + "m";

            //Time of flight
            timeOfFlight = 2 * ((velocityMagnitude * Mathf.Sin(powerIndicator.angle * Mathf.Deg2Rad)) / 9.81f);
            timeOfFlightText.text = timeOfFlight.ToString("F2") + "s";
  
        }

        //Hit or Miss
        HitOrMissText.text = wolverineCollision.hitOrMiss;
        Debug.Log("Text: " + HitOrMissText.text);



    }
}
