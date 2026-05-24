using UnityEngine;
using UnityEngine.SceneManagement;

public class WireCollision : MonoBehaviour
{
    EdgeCollider2D col;
    [SerializeField] GameObject start;

    void Awake()
    {
        col = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = start.transform.position;
    }
}
