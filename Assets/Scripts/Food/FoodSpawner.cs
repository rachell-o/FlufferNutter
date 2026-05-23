using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    [SerializeField] private Sprite[] foodSprites;

    [SerializeField] private float spawnInterval = 0.5f;

    private Camera cam;
    private float timer;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        float screenHeight = cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        float x = Random.Range(
            cam.transform.position.x - screenWidth,
            cam.transform.position.x + screenWidth
        );

        float y = cam.transform.position.y + screenHeight + 1f;

        GameObject food = Instantiate(foodPrefab, new Vector3(x, y, 0f), Quaternion.identity);

        SpriteRenderer sr = food.GetComponent<SpriteRenderer>();
        sr.sprite = foodSprites[Random.Range(0, foodSprites.Length)];
    }
}