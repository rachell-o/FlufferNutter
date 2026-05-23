using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShoot : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction actShoot;

    [SerializeField]
    GameObject bullet;

    private void Awake()
    {
        _pi = GetComponent<PlayerInput>();
        actShoot = _pi.actions["Attack"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
