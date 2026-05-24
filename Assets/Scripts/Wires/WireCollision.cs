using UnityEngine;
using UnityEngine.SceneManagement;

public class WireCollision : MonoBehaviour
{
    EdgeCollider2D col;
    [SerializeField] GameObject start;

    [SerializeField] AudioClip error;

    void Awake()
    {
        col = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(
                error,
                Camera.main.transform.position,
                1.0f
            );
        collision.gameObject.transform.position = start.transform.position;
    }
}
