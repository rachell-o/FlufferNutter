using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWire : MonoBehaviour
{
    [SerializeField] AudioClip success;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(
                success,
                Camera.main.transform.position,
                1.0f
            );
        SceneManager.LoadScene("SceneGER");
    }
}
