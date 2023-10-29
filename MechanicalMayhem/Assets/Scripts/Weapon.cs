using SWAssets;
using SWAssets.Utils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    public WeaponData weaponData;

	private WeaponManager weaponManager;
    private Transform firePoint;
    private float fireTime;
    private bool isFiring;

	private float reloadTime;
	private bool reloading = false;
    private int currentAmmo;
    private int totalAmmo;

	GameObject workbenchUI;

	private bool pickedUp = false;

	private void Awake()
    {
        currentAmmo = (int)weaponData.maxAmmo;
        totalAmmo = (int)weaponData.maxAmmo * weaponData.startingMags;

		firePoint = GameObject.Find("Player").transform;
		pickedUp = !CompareTag("Weapon");
		weaponManager = transform.parent.GetComponent<WeaponManager>();
    }

	private void Update()
    {
		if (!pickedUp)
			return;

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

		if (!isFiring || GameManager.I.InPuzzle())
			return;

		Shoot();
    }

    private Vector2 GetFireDirection(Vector3 target)
    {
		float bulletSpread = UnityEngine.Random.Range(-weaponData.bulletSpread, weaponData.bulletSpread);
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

		if (weaponData.melee)
		{
			// TODO: Do melee attack animation
			RaycastHit2D hit = Physics2D.Raycast(firePoint.position, fireDirection, weaponData.range, weaponData.whatToHit);
			if (!hit.transform)
                return;

            Attackable attackable = hit.transform.GetComponent<Attackable>();
			if (!attackable)
				return;

			attackable.Damage(weaponData.damage);
			// TODO: Spawn melee hit affect

			isFiring = false;
		}
		else
		{
			// TODO: Spawn Muzzle Flash effect

            // Spawn Bullet
            weaponManager.UpdateWeaponUI();
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
		if (!pickedUp || !workbenchUI)
			return;

		if (!workbenchUI.activeSelf ||
			InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>().x
			> workbenchUI.GetComponent<RectTransform>().sizeDelta.x * 2f + 110f) isFiring = true;
    }

    private void FireCanceled(InputAction.CallbackContext obj)
    {
        isFiring = false;
	}

	private void Reload(InputAction.CallbackContext obj)
	{
		if (totalAmmo <= 0 || reloading || currentAmmo >= weaponData.maxAmmo || !pickedUp)
			return;

        reloading = true;
		reloadTime = 0;
	}

    private void FinishReload()
    {
		reloading = false;

		int amountToReload = (int)weaponData.maxAmmo - currentAmmo;
		if (amountToReload > totalAmmo) 
		{
			currentAmmo += totalAmmo;
			totalAmmo = 0;
		}
		else
		{
			totalAmmo -= amountToReload;
			currentAmmo = (int)weaponData.maxAmmo;
		}

		weaponManager.UpdateWeaponUI();
	}

	private void OnDisable()
	{
		reloadTime = 0;
		InputManager.INPUT_ACTIONS.Main.Fire.started -= FireStarted;
		InputManager.INPUT_ACTIONS.Main.Fire.canceled -= FireCanceled;
		InputManager.INPUT_ACTIONS.Main.Reload.started -= Reload;
	}

	private void OnEnable()
	{
		workbenchUI = gameObject.FindDeactivatedGameObject("Canvas", "WorkbenchUI");
		InputManager.INPUT_ACTIONS.Main.Fire.started += FireStarted;
		InputManager.INPUT_ACTIONS.Main.Fire.canceled += FireCanceled;
		InputManager.INPUT_ACTIONS.Main.Reload.started += Reload;
	}

	public void PickUp()
	{
		pickedUp = true;
		transform.parent = GameObject.Find("Player").transform.Find("WeaponManager");
        weaponManager = transform.parent.GetComponent<WeaponManager>();
        weaponManager.PickUpWeapon(transform);
	}

	public int GetCurrentAmmo() => currentAmmo;
	public int GetTotalAmmo() => totalAmmo;

}
