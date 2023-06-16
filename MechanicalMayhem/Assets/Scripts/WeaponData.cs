using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Data")]
public class WeaponData : ScriptableObject
{

    public float fireRate;
    public float damage;
    public float range;
    public LayerMask whatToHit;

    public bool melee;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

}
