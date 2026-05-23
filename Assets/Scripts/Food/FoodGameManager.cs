using UnityEngine;
using TMPro;

public class FoodGameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 10f;

    [SerializeField] private TMP_Text timerText;

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded)
            return;

        gameTime -= Time.deltaTime;

        if (gameTime <= 0f)
        {
            gameTime = 0f;
            EndGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(gameTime);
    }

    void EndGame()
    {
        gameEnded = true;

        Debug.Log("GAME OVER");

        Time.timeScale = 0f;
    }
}