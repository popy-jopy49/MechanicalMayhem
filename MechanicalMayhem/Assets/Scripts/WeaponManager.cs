using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    private List<Transform> guns = new List<Transform>();
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
            guns.Add(transform.GetChild(i));
            guns[i].gameObject.SetActive(i == currentWeaponIndex);
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

    private void SelectWeapon3(InputAction.CallbackContext obj)
	{
        if (guns.Count <= 3)
            return;
		currentWeaponIndex = 3;
        UpdateCurrentGun();
	}

	private void SelectWeapon2(InputAction.CallbackContext obj)
	{
		if (guns.Count <= 2)
			return;
		currentWeaponIndex = 2;
        UpdateCurrentGun();
	}

	private void SelectWeapon1(InputAction.CallbackContext obj)
	{
		if (guns.Count <= 1)
			return;
		currentWeaponIndex = 1;
        UpdateCurrentGun();
	}

	private void Update()
	{
		UpdateGunUI();
	}

	private void UpdateCurrentGun()
    {
        if (!guns[currentWeaponIndex] || currentWeaponIndex <= 0)
            return;

        transform.GetChild(currentWeaponIndex).SetAsFirstSibling();
        guns.Insert(0, transform.GetChild(0));
        guns.RemoveAt(currentWeaponIndex + 1);
        transform.GetChild(1).SetSiblingIndex(currentWeaponIndex);
        currentWeaponIndex = 0;
        foreach (Transform gun in guns)
        {
            gun.gameObject.SetActive(gun.GetSiblingIndex() == currentWeaponIndex);
		}
		UpdateGunUI();
	}

    private void UpdateGunUI()
	{
		if (guns.Count > 0)
		{
			Weapon gun = guns[0].GetComponent<Weapon>();
			equippedWeapon.sprite = gun.GetWeaponData().icon;
			totalAmmo.text = gun.GetTotalAmmo().ToString();
			currentAmmo.text = gun.GetCurrentAmmo().ToString();
		}
		if (guns.Count > 1)
			firstWeapon.sprite = guns[1].GetComponent<Weapon>().GetWeaponData().icon;
		if (guns.Count > 2)
			secondWeapon.sprite = guns[2].GetComponent<Weapon>().GetWeaponData().icon;
		if (guns.Count > 3)
			thirdWeapon.sprite = guns[3].GetComponent<Weapon>().GetWeaponData().icon;
	}

}
