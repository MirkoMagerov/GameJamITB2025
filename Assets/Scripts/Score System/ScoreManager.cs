using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Slider battleSlider;
    [SerializeField] private float sliderStartValue = 50f;
    [SerializeField] private float sliderMin = 0f;
    [SerializeField] private float sliderMax = 100f;
    [SerializeField] private float sliderDecayRate = 1f;
    [SerializeField] private float winThreshold = 100f;
    [SerializeField] private float loseThreshold = 0f;

    private float currentScore;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentScore = sliderStartValue;
        battleSlider.value = sliderStartValue;
        battleSlider.minValue = sliderMin;
        battleSlider.maxValue = sliderMax;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ApplyWordPlacement(true);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ApplyWordPlacement(false);
        }

        UpdateSlider();
        CheckVictory();
    }

    public void ApplyQTEResult(bool isSuccess, bool isPerfect)
    {
        if (isSuccess) currentScore += isPerfect ? 15f : 10f;
        else currentScore -= 10f;

        UpdateSlider();
        CheckVictory();
    }

    public void ApplyWordPlacement(bool isCorrect)
    {
        currentScore += isCorrect ? 10f : -10f;
        UpdateSlider();
        CheckVictory();
    }

    private void UpdateSlider()
    {
        currentScore = Mathf.Clamp(currentScore, sliderMin, sliderMax);
        battleSlider.value = currentScore;
    }

    private void CheckVictory()
    {
        if (currentScore >= winThreshold)
        {
            Debug.Log("¡Jugador Azul gana la batalla de gallos!");
        }
        else if (currentScore <= loseThreshold)
        {
            Debug.Log("¡Jugador Rojo gana la batalla de gallos!");
        }
    }
}
