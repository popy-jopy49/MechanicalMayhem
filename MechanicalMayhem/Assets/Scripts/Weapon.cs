using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    [SerializeField] private WeaponData weaponData;

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
        if (time < 1 / weaponData.fireRate)
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

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, mousePos, weaponData.range, weaponData.whatToHit);
        if (weaponData.melee)
        {
            // Do animation

            if (!hit.transform)
                return;

            Attackable attackable = hit.transform.GetComponent<Attackable>();
            if (!attackable)
            {
                // Spawn wall hit decal
                return;
            }

            attackable.Damage(weaponData.damage);
            // Spawn hit affect
        }
        else
        {
            // Spawn Bullet
            GameObject bullet = Instantiate(weaponData.bulletPrefab, firePoint.position, Quaternion.Euler(firePoint.up));
            bullet.GetComponent<Bullet>().Setup(firePoint.up * weaponData.bulletSpeed, weaponData.damage);
            Destroy(bullet, 5f);
        }

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
