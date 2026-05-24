using UnityEngine;
using UnityEngine.InputSystem;

public class PanMovement : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction _actMove;
    private InputAction _actPause;
    private Vector2 _movement = new();
    private Rigidbody2D _rb;
    //private SpriteRenderer _sr;
    private Collider2D _col;

    [SerializeField] private float MAX_SPEED = 10f;
    [SerializeField] private float DEFAULT_SPEED = 5f   ;
    [SerializeField] private float SLOW_STOP_SPEED = 1f;

    //void Awake()
    void Start()
    {
        // _pi = GetComponentInParent<PlayerInput>();
        _pi = GameManager.instance.Pi;
        _rb = GetComponent<Rigidbody2D>();
        //_sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
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

        Vector3 camPos = cam.transform.position;
        Vector3 pos = transform.position;

        float screenHeight = cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        Bounds b = _col.bounds;
        float leftBound = camPos.x - screenWidth;
        float rightBound = camPos.x + screenWidth;
        float bottomBound = camPos.y - screenHeight;
        float topBound = camPos.y + screenHeight;

        float deltaX = 0f;
        float deltaY = 0f;

        if (b.min.x < leftBound)
            deltaX = leftBound - b.min.x;

        else if (b.max.x > rightBound)
            deltaX = rightBound - b.max.x;

        if (b.min.y < bottomBound)
            deltaY = bottomBound - b.min.y;

        else if (b.max.y > topBound)
            deltaY = topBound - b.max.y;

        transform.position += new Vector3(deltaX, deltaY, 0f);

        if (deltaX != 0 || deltaY != 0)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
