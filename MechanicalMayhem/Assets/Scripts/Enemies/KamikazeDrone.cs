public class KamikazeDrone : Enemy
{

	protected override bool CheckForFireRate()
	{
		return true;
	}

	protected override void Attack()
	{
		base.Attack();
		Destroy(Instantiate(GameAssets.I.DroneExplosionPrefab, target.position, target.rotation), 3f);
		Destroy(gameObject);
	}

}
