using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float speed;

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.forward, speed);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}

