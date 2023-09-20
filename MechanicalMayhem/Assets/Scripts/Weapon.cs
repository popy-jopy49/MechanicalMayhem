using SWAssets.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    public WeaponData weaponData;

    private Transform firePoint;
    private float fireTime;
    private bool isFiring;

	private float reloadTime;
	private bool reloading = false;
    private int currentAmmo;
    private int totalAmmo;

    private void Start()
    {
        InputManager.INPUT_ACTIONS.Main.Fire.started += FireStarted;
        InputManager.INPUT_ACTIONS.Main.Fire.canceled += FireCanceled;
		InputManager.INPUT_ACTIONS.Main.Reload.started += Reload;

        currentAmmo = weaponData.maxAmmo;
        totalAmmo = weaponData.maxAmmo * weaponData.startingMags;

        firePoint = GameObject.Find("Player").transform;
    }

	private void Update()
    {
        if (fireTime < 1 / weaponData.fireRate)
        {
            fireTime += Time.deltaTime;
            return;
		}

		if (!weaponData.melee)
		{
			if (reloading)
			{
				reloadTime += Time.deltaTime;
				if (reloadTime > weaponData.reloadTime)
					FinishReload();

				return;
			}
			if (currentAmmo <= 0)
            {
				Reload(new InputAction.CallbackContext());
                return;
            }
		}

		if (!isFiring)
			return;

		Shoot();
    }

    private Vector2 GetFireDirection(Vector3 target)
    {
		float bulletSpread = Random.Range(-weaponData.bulletSpread, weaponData.bulletSpread);
        Vector3 targetPos = new(
            target.x + bulletSpread,
            target.y + bulletSpread
            );

        Vector3 dir = (targetPos - firePoint.position).normalized;
        return dir;
    }

    private void Shoot()
    {
		currentAmmo--;
		fireTime = 0;

		Vector3 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
		mousePos.z = 0.0f;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		Vector2 fireDirection = GetFireDirection(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, fireDirection, weaponData.range, weaponData.whatToHit);

		if (weaponData.melee)
		{
			// Do animation

			if (!hit.transform)
				return;

            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemies"))
                return;

            Attackable attackable = hit.transform.GetComponent<Attackable>();
			if (!attackable)
				return;

			attackable.Damage(weaponData.damage);
			// Spawn hit affect

			isFiring = false;
		}
		else
		{
			// Spawn Muzzle Flash

			// Spawn Bullet
			Quaternion bulletDirection = Quaternion.Euler(new Vector3(0, 0, VectorUtils.GetAngleFromVector(fireDirection) - 90f));
			GameObject bullet = Instantiate(weaponData.bulletPrefab, firePoint.position, bulletDirection);
			bullet.GetComponent<Bullet>().Setup(fireDirection * weaponData.bulletSpeed, weaponData.damage, weaponData.explosionRadius, weaponData.whatToHit, "PlayerBullet");
			Destroy(bullet, 5f);
			if (weaponData.semiAuto)
				isFiring = false;
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

	private void Reload(InputAction.CallbackContext obj)
	{
		if (totalAmmo <= 0 || reloading || currentAmmo >= weaponData.maxAmmo)
			return;

        reloading = true;
		reloadTime = 0;
	}

    private void FinishReload()
    {
		reloading = false;

		int amountToReload = weaponData.maxAmmo - currentAmmo;
		if (amountToReload > totalAmmo) 
		{
			currentAmmo += totalAmmo;
			totalAmmo = 0;
		}
		else
		{
			totalAmmo -= amountToReload;
			currentAmmo = weaponData.maxAmmo;
		}
    }

	private void OnDisable()
	{
		reloadTime = 0;
	}

	public int GetCurrentAmmo() => currentAmmo;
	public int GetTotalAmmo() => totalAmmo;

}
