using UnityEngine;
using TMPro;

public class FoodScoreManager : MonoBehaviour
{
    public static FoodScoreManager Instance;

    [SerializeField] private int score = 0;
    [SerializeField] private TMP_Text scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }
}