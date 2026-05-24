using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [SerializeField] GameObject failText;
    [SerializeField] GameObject winText;
    [SerializeField] SpriteRenderer black;

    Color color;

    bool failed = false;

    public void Win()
    {
        StartCoroutine(Display());
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(12f);

        winText.SetActive(true);
        StartCoroutine(GoodEnding());
    }

    IEnumerator GoodEnding()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SceneGEA");
    }

    public void Fail()
    {
        Time.timeScale = 0;
        failText.SetActive(true);

        failed = true;

        StartCoroutine(BadEnding());
    }

    IEnumerator BadEnding()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("SceneBEA");
    }

    private void Update()
    {
        if (failed)
        {
            color = black.color;
            color.a = Mathf.Lerp(color.a, 255f, 0.00001f);
            black.color = color;

            Debug.Log(black.color.a);
            if (black.color.a >= 2.5)
            {
                Time.timeScale = 1;
                StartCoroutine(BadEnding());
            }
        }
    }

}
