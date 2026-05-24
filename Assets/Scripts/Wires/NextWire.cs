using UnityEngine;

public class NextWire : MonoBehaviour
{
    [SerializeField] GameObject nextPath;
    [SerializeField] Transform nextStart;
    [SerializeField] GameObject cursor;
    GameObject parent;

    [SerializeField] TimerManager timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nextPath.SetActive(true);
        timer.RestartTimer();
        parent = this.transform.root.gameObject;
        cursor.GetComponent<Rigidbody2D>().angularVelocity = 0;
        cursor.transform.position = nextStart.position;
        Destroy(parent);
    }
}
