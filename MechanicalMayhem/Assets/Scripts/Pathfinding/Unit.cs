using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

	protected Vector2[] path;
    protected int targetIndex;

    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float rotateSpeed = 1f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;
    protected float time;
    protected Transform target;

    protected virtual void Start()
	{
		StartCoroutine(RefreshPath());
	}

	protected virtual IEnumerator RefreshPath()
	{
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially
		
		while (true)
		{
			if (targetPositionOld != (Vector2)target.position)
			{
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath(transform.position, target.position);
				StopCoroutine("FollowPath");
				StartCoroutine("FollowPath");
			}

			yield return new WaitForSeconds(.25f);
		}
	}

    protected virtual IEnumerator FollowPath()
	{
		if (path.Length <= 0)
			yield break;

		targetIndex = 0;
		Vector2 currentWaypoint = path[0];

		while (true)
		{
			if ((Vector2)transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
					yield break;

				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
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

    protected virtual void OnDrawGizmos()
	{
		if (path == null)
			return;

		for (int i = targetIndex; i < path.Length; i ++)
		{
			Gizmos.color = Color.black;
			//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

			if (i == targetIndex)
				Gizmos.DrawLine(transform.position, path[i]);
			else
				Gizmos.DrawLine(path[i-1],path[i]);
		}
	}
}
