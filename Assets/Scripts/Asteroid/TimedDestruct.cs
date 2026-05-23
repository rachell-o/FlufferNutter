using System.Collections;
using UnityEngine;

public class TimedDestruct : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(SelfDestruct(delay));   
    }

    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }
}
