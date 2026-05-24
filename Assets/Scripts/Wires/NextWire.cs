using UnityEngine;

public class NextWire : MonoBehaviour
{
    [SerializeField] GameObject nextPath;
    [SerializeField] Transform nextStart;
    [SerializeField] GameObject cursor;
    GameObject parent;

    [SerializeField] TimerManager timer;

    [SerializeField] AudioClip success;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(
                success,
                Camera.main.transform.position,
                1.0f
            );
        nextPath.SetActive(true);
        timer.RestartTimer();
        parent = this.transform.root.gameObject;
        cursor.GetComponent<Rigidbody2D>().angularVelocity = 0;
        cursor.transform.position = nextStart.position;
        Destroy(parent);
    }
}
