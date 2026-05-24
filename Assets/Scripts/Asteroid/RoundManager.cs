using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] GameObject failText;
    [SerializeField] GameObject winText;

    public void Win()
    {
        StartCoroutine(Display());
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(12f);

        winText.SetActive(true);
    }

    public void Fail()
    {
        Time.timeScale = 0;
        failText.SetActive(true);
    }


}
