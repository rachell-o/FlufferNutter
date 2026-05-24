using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    //[SerializeField] private Sprite[] foodSprites;
    [SerializeField] private Sprite[] goodSprites;
    [SerializeField] private Sprite[] badSprites;

    [SerializeField] private float spawnInterval = 0.5f;

    private Camera cam;
    private float timer;
    private bool gameEnded = false;
    private bool canSpawn = true; //IF YOU WANT THE GAME TO BE PAUSED BEFORE YOU START, CHANGE TO FALSE.

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!canSpawn)
        return;
        
        if (gameEnded)
        return;
        
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
        //sr.sprite = foodSprites[Random.Range(0, foodSprites.Length)];
        FoodCollision fc = food.GetComponent<FoodCollision>();

        bool isGood = Random.value > 0.5f;

        fc.SetGood(isGood);

        if (isGood)
            sr.sprite = goodSprites[Random.Range(0, goodSprites.Length)];
        else
            sr.sprite = badSprites[Random.Range(0, badSprites.Length)];
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }

    public void StopSpawning()
    {
        gameEnded = true;
    }
}