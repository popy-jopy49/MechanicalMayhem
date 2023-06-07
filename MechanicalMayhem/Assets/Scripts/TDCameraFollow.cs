using UnityEngine;

public class TDCameraFollow : MonoBehaviour
{

    [SerializeField] private float smoothTime;
    private Transform player;
    private Vector2 currentVelocity;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector2.SmoothDamp(transform.position, player.position, ref currentVelocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

}
