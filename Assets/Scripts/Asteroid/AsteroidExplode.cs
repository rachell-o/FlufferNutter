using System.Collections;
using UnityEngine;

public class AsteroidExplode : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject flare;
    private GameObject bullet;
    private bool isExploding = false;

    [SerializeField] RoundManager manager;
    
    void Start()
    {
        Invoke("AddAnimator", 0.5f);
    }

    void AddAnimator()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("detect");
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
        if(collision.tag == "Player")
        {
            Debug.Log("pplat");
            manager.Fail();
        }
    }

    void Explode()
    {
        isExploding = true;
        if(animator != null)
        {
            animator.SetTrigger("Explode");
        }
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.4f);

        Destroy(transform.root.gameObject);
    }
}
