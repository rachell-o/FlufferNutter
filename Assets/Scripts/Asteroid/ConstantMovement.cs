using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] 
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = this.transform.up * speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        _rb.linearVelocity = this.transform.up * speed;
    }
}
