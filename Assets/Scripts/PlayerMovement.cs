using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction _actMove;
    private InputAction _actPause;
    private Vector2 _movement = new();
    private Rigidbody2D _rb;

    [SerializeField] private float MAX_SPEED = 10f;
    [SerializeField] private float DEFAULT_SPEED = 5;
    [SerializeField] private float SLOW_STOP_SPEED = 1f;


    void Awake()
    {
        _pi = GetComponentInParent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _actMove = _pi.actions["Move"];
    }

    void Update()
    {
        _movement = _actMove.ReadValue<Vector2>();
        // Debug.Log(_actMove.ReadValue<Vector2>());
        // if(_movement.x > 0)
        // {
        //     Debug.Log(_movement.x);
        //     Debug.Log(_movement.y);
        // }

        if (_movement.x != 0 || _movement.y != 0)
        {
            _rb.AddForce(_movement.normalized * DEFAULT_SPEED, ForceMode2D.Force);
            // _rb.MovePosition(_movement.normalized * 1f);
            // _rb.linearVelocity = _movement.normalized * 1f;
            // gameObject.transform.Translate(_movement * 8f * Time.deltaTime);
            if(_rb.linearVelocity.magnitude > MAX_SPEED)
            {
                _rb.linearVelocity = _movement.normalized * MAX_SPEED;
            }
        }
        else
        {
            _rb.AddForce(_rb.linearVelocity * -SLOW_STOP_SPEED, ForceMode2D.Force);
        }
    }
}
