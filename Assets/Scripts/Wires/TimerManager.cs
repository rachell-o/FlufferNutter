using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float timeGiven = 60;
    [SerializeField] string title = "Time Left: ";
    float timeRemaining;
    bool timerIsRunning = false;
    [SerializeField] TMP_Text timer;
    void Awake()
    {
        RestartTimer();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            Debug.Log("1");
            if (timeRemaining >= 0)
            {
                Debug.Log("2");
                timeRemaining -= Time.deltaTime;
                DisplayTime();
            }
            else
            {
                Debug.Log("3");
                timer.text = title;
                timerIsRunning = false;
            }
        }
        else
        {
            Debug.Log("4");
            timer.text += "-";
        }
        if (timer.text == title + "-----------------------------------------------------------------")
        {
            SceneManager.LoadScene("SceneBER");
        }
    }

    public void RestartTimer()
    {
        timerIsRunning = true;
        timeRemaining = timeGiven;
    }

    void DisplayTime()
    {
        float seconds = Mathf.FloorToInt(timeRemaining);
        timer.text = title + seconds;
    }
}
