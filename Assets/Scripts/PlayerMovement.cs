using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction _actMove;
    private InputAction _actPause;
    private Vector2 _movement = new();
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    [SerializeField] private float MAX_SPEED = 10f;
    [SerializeField] private float DEFAULT_SPEED = 5f;
    [SerializeField] private float SLOW_STOP_SPEED = 1f;


    void Awake()
    {
        _pi = GetComponentInParent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _actMove = _pi.actions["Move"];
    }

    void FixedUpdate()
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
                _rb.linearVelocity = _rb.linearVelocity.normalized * MAX_SPEED;
            }
        }
        else
        {
            _rb.AddForce(_rb.linearVelocity * -SLOW_STOP_SPEED, ForceMode2D.Force);
        }
        
        ClampToScreen();
    }
    
    void ClampToScreen()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        Vector3 camPos = cam.transform.position;
        Vector3 pos = transform.position;

        // Half-size of the sprite
        float halfWidth = _sr.bounds.extents.x;
        float halfHeight = _sr.bounds.extents.y;

        bool clamped = false;

        // Calculate actual screen bounds
        float leftBound = camPos.x - screenWidth + halfWidth;
        float rightBound = camPos.x + screenWidth - halfWidth;
        float bottomBound = camPos.y - screenHeight + halfHeight;
        float topBound = camPos.y + screenHeight - halfHeight;

        // Clamp horizontally
        if (pos.x < -screenWidth + halfWidth)
        {
            pos.x = -screenWidth + halfWidth;
            clamped = true;
        }
        else if (pos.x > screenWidth - halfWidth)
        {
            pos.x = screenWidth - halfWidth;
            clamped = true;
        }

        // Clamp vertically
        if (pos.y < -screenHeight + halfHeight)
        {
            pos.y = -screenHeight + halfHeight;
            clamped = true;
        }
        else if (pos.y > screenHeight - halfHeight)
        {
            pos.y = screenHeight - halfHeight;
            clamped = true;
        }

        transform.position = pos;

        // Stop movement when hitting edge
        if (clamped)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
