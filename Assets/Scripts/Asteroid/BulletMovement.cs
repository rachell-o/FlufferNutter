using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] 
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
