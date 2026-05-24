using System.Collections.Generic;
using UnityEngine;

public class RandomAsteroidSelector : MonoBehaviour
{
    [SerializeField] List<GameObject> asteroids;
    private Transform child;
    private GameObject asteroid;
    private int randomAsteroid;
    private float randomSize;

    void Awake()
    {
        randomAsteroid = Random.Range(0, asteroids.Count);
        randomSize = Random.Range(0.25f, 1.5f);
        child = transform.GetChild(0);
        asteroid = Instantiate(asteroids[randomAsteroid], transform.position, asteroids[randomAsteroid].transform.rotation);
        asteroid.transform.parent = child;
        asteroid.transform.localScale = new Vector2(randomSize, randomSize);
    }
}
