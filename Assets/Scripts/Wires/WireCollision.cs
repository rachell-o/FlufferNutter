using UnityEngine;
using UnityEngine.SceneManagement;

public class WireCollision : MonoBehaviour
{
    EdgeCollider2D col;
    void Awake()
    {
        col = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("SceneBER");
    }
}
