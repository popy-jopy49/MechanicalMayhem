using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Attackable
{

    [Header("Basic Enemy Values")]
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;

    [Header("Navigation Values")]
    [SerializeField] protected float distanceToStop = 2.25f;
    [SerializeField] protected float distanceToSeek = 3.5f;

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

        agent.isStopped = sqrDist <= distanceToStop * distanceToStop;
        agent.SetDestination(target.position);

        if (!agent.isStopped)
            playerVDist = agent.desiredVelocity;

		float rot = Mathf.Atan2(playerVDist.y, playerVDist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot - 90f), Time.deltaTime * agent.angularSpeed);

		if (agent.isStopped)
		{
			if (!CheckForFireRate())
				return;

			Attack();
		}
	}

    protected virtual void Attack()
    {
        target.GetComponent<Player>().Damage(damage);
    }

    protected bool CheckForFireRate()
    {
        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return false;
        }

        time = 0;
        return true;
    }

}
