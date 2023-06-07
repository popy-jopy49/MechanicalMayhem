using UnityEngine;

public class Wrench : MonoBehaviour
{

    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatToHit;
    private float time;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (time < fireRate)
        {
            time += Time.deltaTime;
            return;
        }

        time = 0;
        /*if (Physics2D.Raycast(transform.position, transform.up, range, whatToHit, ))
        {

        }*/
    }

}
