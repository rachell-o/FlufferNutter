using System.Collections;
using UnityEngine;

public class AsteroidExplode : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject flare;
    private GameObject bullet;
    private bool isExploding = false;
    
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExploding)
        {
            return;
        }

        if(collision.tag == "Bullet")
        {
            bullet = collision.gameObject;

            Explode();

            Instantiate(flare, bullet.transform.position + (bullet.transform.up * -0.2f), bullet.transform.rotation);
            Destroy(bullet);
        }
    }

    void Explode()
    {
        isExploding = true;
        animator.SetTrigger("Explode");
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.4f);

        Destroy(transform.root.gameObject);
    }
}
