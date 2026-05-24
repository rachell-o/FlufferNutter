using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShoot : MonoBehaviour
{
    PlayerInput _pi;
    private InputAction actShoot;
    private bool coolingDown;


    [SerializeField] GameObject bullet;
    [SerializeField] private float coolDownTime = 0.5f;
    [SerializeField] GameObject muzzleFlare;
    private float muzzleFlareDistance = 0.3f;

    private void Awake()
    {
        _pi = GetComponent<PlayerInput>();
        actShoot = _pi.actions["Attack"];
    }

    // Update is called once per frame
    void Update()
    {
        if (actShoot.IsPressed())
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (!coolingDown)
        {
            Shoot();
            StartCoroutine(CoolDown(coolDownTime));
        }
    }

    void Shoot()
    {
        coolingDown = true;

        Instantiate(bullet, transform.position, transform.rotation);



        Instantiate(muzzleFlare, transform.position + (transform.up * muzzleFlareDistance), transform.rotation);
    }

    IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);

        coolingDown = false;
    }
}
