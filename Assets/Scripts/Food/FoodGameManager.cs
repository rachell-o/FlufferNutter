using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FoodGameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 10f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text finalScoreText;
    
    [SerializeField] private GameObject skillet;
    //[SerializeField] private GameObject cursorHandPrefab;
    //[SerializeField] private Transform cursorSpawnPoint;
    [SerializeField] private GameObject instructionPanel;

    private bool gameStarted = true; // IF YOU WANT A PAUSE BEFORE THE GAME STARTS, CHANGE THIS TO FALSE
    private bool gameEnded = false;

    void Update()
    {
        if (!gameStarted)
        return;

        // if (waitingForContinue)
        // {
        //     if (_continueAction.triggered)
        //     {
        //         LoadMainMenu();
        //     }
        //     return;
        // }
        
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

    public void StartGame()
    {
        gameStarted = true;

        instructionPanel.SetActive(false);

        FindAnyObjectByType<FoodSpawner>()?.StartSpawning();
    }

    void EndGame()
    {
        gameEnded = true;

        FindAnyObjectByType<FoodSpawner>()?.StopSpawning();

        FoodFall[] foods = FindObjectsByType<FoodFall>();

        foreach (FoodFall food in foods)
        {
            Destroy(food.gameObject);
        }

        Destroy(skillet);

        //Instantiate(
        //    cursorHandPrefab,
        //    cursorSpawnPoint.position,
        //    Quaternion.identity
        //);

        endScreen.SetActive(true);

        finalScoreText.text =
            "Final Score: " + FoodScoreManager.Instance.GetScore();
    }

    public void LoadMainMenu()
    {
        //Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}