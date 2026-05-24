using UnityEngine;

public class SparkGlitch : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 20) == 0)
        {
            animator.SetTrigger("Glitch");
        }
    }
}
