using UnityEngine;
using UnityEngine.InputSystem;

public class RocketAnimation : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction actMove;
    private Vector2 movement;

    private Animator animator; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // _pi = GetComponent<PlayerInput>();
        _pi = GameManager.instance.Pi;
        animator = GetComponent<Animator>();
        actMove = _pi.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        movement = actMove.ReadValue<Vector2>();

        animator.SetBool(
            "isMoving",
            (movement.x != 0 || movement.y != 0));
    }
}
