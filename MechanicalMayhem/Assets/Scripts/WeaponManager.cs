using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private List<Transform> guns = new List<Transform>();
    private int currentWeaponIndex = 0;
    private Transform currentWeapon;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            guns.Add(transform.GetChild(i));
        }

        currentWeapon = guns[currentWeaponIndex];
    }

    private void Update()
    {
        

    }

}
