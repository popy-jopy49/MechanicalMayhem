using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Data")]
public class WeaponData : ScriptableObject
{

    public Sprite icon;
    public float fireRate;
    public float damage;
    public float range;
    public LayerMask whatToHit;

    public bool melee;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
	public int maxAmmo;
	public int startingMags;
    public float reloadTime;
    public float explosionRadius = 0;
    public bool semiAuto = false;

}
