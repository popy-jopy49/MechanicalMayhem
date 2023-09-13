using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : Attackable
{

    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float rotateSpeed = 1f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;

    protected float time;
    protected Transform target;
    protected Vector2[] path;
    protected int targetIndex;
    protected NavMeshAgent agent;

    protected virtual void Awake()
	{
		target = GameObject.Find("Player").transform;
	}

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
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
