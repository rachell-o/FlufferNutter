using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FoodGameManager : MonoBehaviour
{
    [SerializeField] private SONavigation _nav;
    [SerializeField] private float gameTime = 30f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text finalScoreText;
    
    [SerializeField] private GameObject skillet;
    [SerializeField] private GameObject cursorHand;
    //[SerializeField] private Transform cursorSpawnPoint;
    [SerializeField] private GameObject instructionPanel;

    private bool gameStarted = true; // IF YOU WANT A PAUSE BEFORE THE GAME STARTS, CHANGE THIS TO FALSE
    private bool gameEnded = false;
    private string nextSceneName;
    

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
        cursorHand.SetActive(true);
        // if Win
        // _nav.GetSceneByName(somethingstring)
        // if lose
        // _nav.GetSceneByName(somethingstring)

        //Instantiate(
        //    cursorHandPrefab,
        //    cursorSpawnPoint.position,
        //    Quaternion.identity
        //);

        endScreen.SetActive(true);
      
        int finalScore = FoodScoreManager.Instance.GetScore();
        if (finalScore > 0)
        {
            nextSceneName = "SceneGEZ";
        }
        else
        {
            nextSceneName = "SceneBEZ";
        }
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void LoadMainMenu()
    {
        //Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextScene()
    {
        _nav.GetSceneByName(nextSceneName);
    }
}