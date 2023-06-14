using UnityEngine;
using UnityEngine.InputSystem;

public class Wrench : MonoBehaviour
{

    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private bool melee = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;

    private Transform firePoint;
    private float time;
    private bool isFiring;

    private void Start()
    {
        InputManager.INPUT_ACTIONS.Main.Fire.started += FireStarted;
        InputManager.INPUT_ACTIONS.Main.Fire.canceled += FireCanceled;

        firePoint = transform.Find("FirePoint");
    }

    private void Update()
    {
        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return;
        }

        if (!isFiring)
            return;

        time = 0;
        
        Vector3 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= firePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, mousePos, range, whatToHit);
        if (melee)
        {
            // Do animation
        }
        else
        {
            // Spawn Bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(mousePos));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.forward * bulletSpeed);
            Destroy(bullet, 5f);
        }

        if (!hit.transform)
            return;

        Attackable attackable = hit.transform.GetComponent<Attackable>();
        if (!attackable)
        {
            // Spawn wall hit decal
            return;
        }

        attackable.Damage(damage);
        // Spawn hit affect
    }

    private void FireStarted(InputAction.CallbackContext obj)
    {
        isFiring = true;
    }

    private void FireCanceled(InputAction.CallbackContext obj)
    {
        isFiring = false;
    }

}
