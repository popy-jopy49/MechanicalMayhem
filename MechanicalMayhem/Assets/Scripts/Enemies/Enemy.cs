using UnityEngine;
using UnityEngine.AI;

public class Enemy : Attackable
{

    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float stopDistance = 2.25f;
    [SerializeField] protected float seekDistance = 3.5f;

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
		float sqrDist = (playerVDist).sqrMagnitude;
        if (sqrDist > seekDistance * seekDistance)
            return;

        agent.isStopped = sqrDist <= stopDistance * stopDistance;
        agent.SetDestination(target.position);

        Vector3 vDir = agent.desiredVelocity;
        if (agent.isStopped)
            vDir = playerVDist;

		float rot = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot - 90f), Time.deltaTime * agent.angularSpeed);
	}

    protected virtual void Attack()
    {
        if (!CheckForFireRate())
            return;

        target.GetComponent<Player>().Damage(damage);
    }

    protected bool CheckForFireRate()
    {
        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return false;
        }
        else
        {
            time = 0;
            return true;
        }
    }

}
