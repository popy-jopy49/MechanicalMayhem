public class KamikazeDrone : Enemy
{

	// Always be willing to blow up
	protected override bool CheckForFireRate()
	{
		return true;
	}

	// Attack player and spawn hit explosion effect
	protected override void Attack()
	{
		base.Attack();
		Destroy(Instantiate(GameAssets.I.DroneExplosionPrefab, target.position, target.rotation), 3f);
		Destroy(gameObject);
	}

}
