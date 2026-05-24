using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction _actMove;
    private InputAction _actPause;
    private InputAction _space;
    private Vector2 _movement = new();
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Camera _cam;
    GameObject _buttonPressedRef;
    private Vector2 _velocity;
    private float MAX_SPEED = 11f;
    private float DEFAULT_SPEED = 10f;
    private float SLOW_STOP_SPEED = 3f;


    void Start()
    {
        _pi = GameManager.instance.Pi;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _actMove = _pi.actions["Move"];
        _space = _pi.actions["Attack"];
        _cam = GameManager.instance.Camera;
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
            if (_rb.linearVelocity.magnitude > MAX_SPEED)
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

    void Update()
    {
        _velocity = _rb.linearVelocity;
        if (_velocity != Vector2.zero)
        {
            transform.up = _velocity;
        }

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(_cam, transform.position);
        Ray rayon = _cam.ScreenPointToRay(screenPos);
        RaycastHit raycastHit;
        Debug.DrawRay(rayon.origin, rayon.direction * 10, Color.yellow);

        bool weHitSomething = Physics.Raycast(rayon, out raycastHit);

        if (weHitSomething)
        {
            if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Button"))
            {
                // Debug.Log("We're over a button!!!");
                _buttonPressedRef = raycastHit.collider.gameObject;
                _buttonPressedRef.GetComponent<ButtonForCursor>().ChangeSpirte(true);

                if (_space.triggered)
                {
                    _buttonPressedRef.GetComponent<ButtonForCursor>().TriggerEvent();
                }
            }
        }
        else if (_buttonPressedRef != null)
        {
            // Debug.Log("We are not over the button anymore");
            _buttonPressedRef.GetComponent<ButtonForCursor>().ChangeSpirte(false);
            _buttonPressedRef = null;
        }
    }

    void ClampToScreen()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;
        // Debug.Log(screenHeight + " screenHeight");
        // Debug.Log(screenWidth + " screenWidth");

        Vector3 camPos = cam.transform.position;
        Vector3 pos = transform.position;

        // Half-size of the sprite
        float halfWidth = _sr.bounds.extents.x;
        float halfHeight = _sr.bounds.extents.y;

        bool clamped = false;

        // Calculate actual screen bounds
        // float leftBound = camPos.x - screenWidth + halfWidth;
        // float rightBound = camPos.x + screenWidth - halfWidth;
        // float bottomBound = camPos.y - screenHeight + halfHeight;
        // float topBound = camPos.y + screenHeight - halfHeight;

        float offsetH = 1.2f;

        // Clamp horizontally
        if (pos.x < -screenWidth + offsetH)
        {
            pos.x = -screenWidth + offsetH;
            clamped = true;
        }
        else if (pos.x > screenWidth - offsetH)
        {
            pos.x = screenWidth - offsetH;
            clamped = true;
        }

        // Clamp vertically
        if (pos.y < -screenHeight + 2)
        // if (pos.y < -screenHeight + halfHeight)
        {
            // pos.y = -screenHeight + halfHeight;
            pos.y = -screenHeight + 2;
            clamped = true;
        }
        // else if (pos.y > screenHeight - halfHeight)
        else if (pos.y > screenHeight)
        {
            // pos.y = screenHeight - halfHeight;
            pos.y = screenHeight;
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
