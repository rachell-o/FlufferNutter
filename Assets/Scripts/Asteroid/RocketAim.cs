using UnityEngine;

public class RocketAim : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = _rb.linearVelocity;
        if(velocity != Vector2.zero)
        {
            transform.up = velocity;
        }
    }
}
