using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] RoundManager manager;

    [SerializeField] GameObject asteroid;
    GameObject newAsteroid;

    [SerializeField] float angleRange;
    [SerializeField] float positionRange = 15f;
    [SerializeField] float intervalUpper = 0.25f;
    [SerializeField] float intervalLower = 1.5f;

    float rndPosition;
    float rndAngle;
    float rndInterval;

    float x;
    float y;
    Vector2 initAngle;

    [SerializeField] int totalAst = 30;
    int sumAst = 0;

    void Awake()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        rndInterval = Random.Range(intervalLower, intervalUpper);
        yield return new WaitForSeconds(rndInterval);

        rndPosition = Random.Range(0f, 2*Mathf.PI);
        x = positionRange * Mathf.Cos(rndPosition);
        y = positionRange * Mathf.Sin(rndPosition);

        

        rndAngle = Random.Range(-angleRange, angleRange);

        initAngle =  new Vector3(x,y,0) - this.transform.position;

        float angle = Mathf.Atan2(initAngle.y, initAngle.x) * Mathf.Rad2Deg;

        newAsteroid = Instantiate(asteroid, new Vector3(x, y, 0), Quaternion.Euler(0, 0, angle+rndAngle+90));

        sumAst++;
       
        if(sumAst < totalAst)
        {
            StartCoroutine(Spawn());
        }
        else
        {
            manager.Win();
        }
    }
}
