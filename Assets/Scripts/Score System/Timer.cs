using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Image fillImage;

    // Shake parameters
    [SerializeField] private float shakeStartTime = 0.4f;
    [SerializeField] private float shakeIntensity = 5f;
    [SerializeField] private float shakeSpeed = 10f;

    private RectTransform sliderRect;
    private Vector3 originalPosition;

    void Start()
    {
        timeSlider.maxValue = timeLimit;
        timeSlider.value = timeLimit;
        timeSlider.minValue = 0f;

        sliderRect = timeSlider.GetComponent<RectTransform>();
        originalPosition = sliderRect.localPosition;
    }

    void Update()
    {
        if (timeSlider.value == 0f)
        {
            ScoreManager.Instance.ApplyWordPlacement(false);
            Debug.Log("Time's up! You lose!");
            timeSlider.value = timeLimit;
            sliderRect.localPosition = originalPosition;
        }
        else
        {
            timeSlider.value -= Time.deltaTime;
        }

        float normalizedTime = timeSlider.value / timeSlider.maxValue;

        // Color verde para tiempo alto
        Color green = new Color(0.1f, 0.9f, 0.1f);
        // Color amarillo para tiempo medio
        Color yellow = new Color(1.0f, 0.9f, 0.0f);
        // Color rojo intenso para tiempo bajo
        Color red = new Color(1.0f, 0.0f, 0.0f);

        // Transición con umbral ajustado para mostrar más rojo
        if (normalizedTime > 0.65f)
        {
            // Verde a amarillo (mayor parte superior)
            fillImage.color = Color.Lerp(yellow, green, (normalizedTime - 0.65f) / 0.35f);
        }
        else
        {
            // Amarillo a rojo (menor parte inferior - más visible)
            fillImage.color = Color.Lerp(red, yellow, normalizedTime / 0.65f);
        }

        HandleShaking(normalizedTime);
    }

    public void HandleShaking(float normalizedTime)
    {
        if (normalizedTime < shakeStartTime)
        {
            float shakeAmount = (shakeStartTime - normalizedTime) / shakeStartTime * 1.5f; // Stronger baseline

            // More chaotic, erratic shake pattern
            Vector3 shakeOffset = new Vector3(
                Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity * shakeAmount +
                Mathf.Sin(Time.time * shakeSpeed * 2.3f) * shakeIntensity * 0.4f * shakeAmount,

                Mathf.Cos(Time.time * shakeSpeed * 1.7f) * shakeIntensity * shakeAmount +
                Mathf.Cos(Time.time * shakeSpeed * 3.1f) * shakeIntensity * 0.3f * shakeAmount,

                0
            );
            sliderRect.localPosition = originalPosition + shakeOffset;
        }
        else
        {
            sliderRect.localPosition = originalPosition;
        }
    }
}
