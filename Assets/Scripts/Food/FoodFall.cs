using UnityEngine;

public class FoodFall : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float bottom = cam.transform.position.y - cam.orthographicSize - 2f;

        if (transform.position.y < bottom)
        {
            Destroy(gameObject);
        }
    }
}