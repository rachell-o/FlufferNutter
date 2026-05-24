using UnityEngine;

public class NextWire : MonoBehaviour
{
    [SerializeField] GameObject nextPath;
    [SerializeField] Transform nextStart;
    [SerializeField] GameObject cursor;
    GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nextPath.SetActive(true);

        parent = this.transform.root.gameObject;
        cursor.transform.position = nextStart.position;
        Destroy(parent);
    }
}
