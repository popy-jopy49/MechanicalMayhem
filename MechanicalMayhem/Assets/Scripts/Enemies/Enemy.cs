using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Attackable
{

    [Header("Basic Enemy Values")]
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected int nutsAndBoltsOnDrop = 10;

    [Header("Navigation Values")]
    [SerializeField] protected float distanceToStop = 2.25f;
    [SerializeField] protected float distanceToSeek = 10f;

    [Header("Layers")]
	[SerializeField] private LayerMask everthingButSelf;

	protected float time;
    protected Transform target;
    protected NavMeshAgent agent;

    protected virtual void Awake()
	{
		target = GameObject.Find("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

    protected virtual void Update()
    {
        Vector2 playerVDist = target.position - transform.position;
		float sqrDist = playerVDist.sqrMagnitude;
        if (sqrDist > distanceToSeek * distanceToSeek)
            return;
		RaycastHit2D hit2D = Physics2D.Raycast(transform.position, playerVDist, 50f, everthingButSelf);
		if (!hit2D.transform || !hit2D.transform.gameObject.CompareTag("Player"))
			return;

        agent.SetDestination(target.position);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		agent.isStopped = sqrDist <= distanceToStop * distanceToStop;

		float rot = Mathf.Atan2(playerVDist.y, playerVDist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot - 90f), Time.deltaTime * agent.angularSpeed);

		if (!agent.isStopped || !CheckForFireRate())
				return;

		Attack();
	}

    protected virtual void Attack()
    {
        target.GetComponent<Player>().Damage(damage);
    }

    protected virtual bool CheckForFireRate()
    {
        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return false;
        }

        time = 0;
        return true;
    }

	protected override void Die()
	{
        Destroy(Instantiate(GameAssets.I.EnemyDeathEffect, transform.position, transform.rotation), 3f);
        PlayerStats.I.ChangeNutsAndBolts(nutsAndBoltsOnDrop);
        base.Die();
	}

}
