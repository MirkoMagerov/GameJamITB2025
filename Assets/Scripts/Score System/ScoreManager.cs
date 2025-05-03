using System;
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
    [SerializeField] private float leftPlayerWinPoints = 100f;
    [SerializeField] private float rightPlayerWinPoints = 0f;

    public static Action<int> OnEndOfGame;

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
        UpdateSlider();
        CheckVictory();
    }

    public void ApplyQTEResult(int score, bool isLeftPlayer)
    {
        // Si el jugador es el de la derecha, invertimos la dirección
        if (!isLeftPlayer)
            score *= -1;

        currentScore += score;
        UpdateSlider();
        CheckVictory();
    }


    public void ApplyWordPlacement(int points, bool leftPLayer)
    {
        points = leftPLayer ? points : -points;
        Debug.Log($"ScoreManager: {points}");
        currentScore += points;

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
        if (currentScore <= leftPlayerWinPoints)
        {
            Debug.Log("¡Jugador Azul gana la batalla de gallos!");
            OnEndOfGame?.Invoke(1);
        }
        else if (currentScore >= rightPlayerWinPoints)
        {
            Debug.Log("¡Jugador Rojo gana la batalla de gallos!");
            OnEndOfGame?.Invoke(-1);
        }
    }
}
