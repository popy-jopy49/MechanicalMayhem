using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    private List<Transform> weapons = new();
    private int currentWeaponIndex = 0;

    private Image firstWeapon;
    private Image secondWeapon;
    private Image thirdWeapon;
    private Image equippedWeapon;
    private TMP_Text totalAmmo;
    private TMP_Text currentAmmo;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons.Add(transform.GetChild(i));
            weapons[i].gameObject.SetActive(i == currentWeaponIndex);
        }

        InputManager.INPUT_ACTIONS.Main.SelectWeapon1.started += SelectWeapon1;
		InputManager.INPUT_ACTIONS.Main.SelectWeapon2.started += SelectWeapon2;
		InputManager.INPUT_ACTIONS.Main.SelectWeapon3.started += SelectWeapon3;

		equippedWeapon = GameObject.Find("EquippedWeapon").transform.Find("Icon").GetComponent<Image>();
		firstWeapon = GameObject.Find("FirstWeapon").transform.Find("Icon").GetComponent<Image>();
		secondWeapon = GameObject.Find("SecondWeapon").transform.Find("Icon").GetComponent<Image>();
		thirdWeapon = GameObject.Find("ThirdWeapon").transform.Find("Icon").GetComponent<Image>();
        currentAmmo = GameObject.Find("CurrentAmmo").GetComponent<TMP_Text>();
        totalAmmo = GameObject.Find("TotalAmmo").GetComponent<TMP_Text>();
    }

    private void Start()
    {
		if (!GameManager.I.newGame)
			return;

		for (int i = 0; i < weapons.Count; i++)
		{
			WeaponData weaponData = weapons[i].GetComponent<Weapon>().weaponData;
			ChangeWeaponDataValues(ref weaponData, Resources.Load<WeaponData>("ScriptableObjects/Defaults/" + weaponData.name.Split('_')[0]));
		}
    }

    private void ChangeWeaponDataValues(ref WeaponData weaponData, WeaponData defaultData)
    {
		if (weaponData == null) return;

		weaponData.icon = defaultData.icon;
		weaponData.fireRate = defaultData.fireRate;
		weaponData.damage = defaultData.damage;
		weaponData.range = defaultData.range;
		weaponData.whatToHit = defaultData.whatToHit;
		weaponData.melee = defaultData.melee;
		weaponData.bulletPrefab = defaultData.bulletPrefab;
		weaponData.bulletSpeed = defaultData.bulletSpeed;
		weaponData.shotCount = defaultData.shotCount;
		weaponData.bulletSpread = defaultData.bulletSpread;
		weaponData.maxAmmo = defaultData.maxAmmo;
		weaponData.startingMags = defaultData.startingMags;
		weaponData.reloadTime = defaultData.reloadTime;
		weaponData.explosionRadius = defaultData.explosionRadius;
		weaponData.semiAuto = defaultData.semiAuto;
		weaponData.enemy = defaultData.enemy;
    }

    private void SelectWeapon3(InputAction.CallbackContext obj)
	{
        if (weapons.Count <= 3)
            return;
		currentWeaponIndex = 3;
        UpdateCurrentWeapon();
	}

	private void SelectWeapon2(InputAction.CallbackContext obj)
	{
		if (weapons.Count <= 2)
			return;
		currentWeaponIndex = 2;
        UpdateCurrentWeapon();
	}

	private void SelectWeapon1(InputAction.CallbackContext obj)
	{
		if (weapons.Count <= 1)
			return;
		currentWeaponIndex = 1;
        UpdateCurrentWeapon();
	}

	private void Update()
	{
		UpdateWeaponUI();
	}

	private void UpdateCurrentWeapon()
    {
        if (!weapons[currentWeaponIndex] || currentWeaponIndex <= 0)
            return;

        transform.GetChild(currentWeaponIndex).SetAsFirstSibling();
        weapons.Insert(0, transform.GetChild(0));
        weapons.RemoveAt(currentWeaponIndex + 1);
        weapons.Insert(currentWeaponIndex + 1, weapons[1]);
		weapons.RemoveAt(1);
        transform.GetChild(1).SetSiblingIndex(currentWeaponIndex);
        currentWeaponIndex = 0;
        foreach (Transform weapon in weapons)
        {
            weapon.gameObject.SetActive(weapon.GetSiblingIndex() == currentWeaponIndex);
		}
		UpdateWeaponUI();
	}

    private void UpdateWeaponUI()
	{
		if (weapons.Count > 0)
		{
			Weapon weapon = weapons[0].GetComponent<Weapon>();
			equippedWeapon.sprite = weapon.weaponData.icon;
            totalAmmo.text = weapon.GetTotalAmmo().ToString();
            currentAmmo.text = weapon.GetCurrentAmmo().ToString();
            totalAmmo.gameObject.SetActive(!weapon.weaponData.melee);
            currentAmmo.gameObject.SetActive(!weapon.weaponData.melee);
		}
		if (weapons.Count > 1)
		{
			firstWeapon.sprite = weapons[1].GetComponent<Weapon>().weaponData.icon;
			firstWeapon.gameObject.SetActive(true);
		}
		else
		{
			firstWeapon.gameObject.SetActive(false);
		}
		if (weapons.Count > 2)
		{
			secondWeapon.sprite = weapons[2].GetComponent<Weapon>().weaponData.icon;
			secondWeapon.gameObject.SetActive(true);
		}
		else
		{
			secondWeapon.gameObject.SetActive(false);
		}
		if (weapons.Count > 3)
		{
			thirdWeapon.sprite = weapons[3].GetComponent<Weapon>().weaponData.icon;
			thirdWeapon.gameObject.SetActive(true);
		}
		else
		{
			thirdWeapon.gameObject.SetActive(false);
		}
	}

}
