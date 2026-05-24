using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("SceneGER");
    }
}
