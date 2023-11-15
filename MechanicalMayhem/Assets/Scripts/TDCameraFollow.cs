using UnityEngine;

public class TDCameraFollow : MonoBehaviour
{

    [SerializeField] private float smoothTime;
    private Transform player;
    private Vector2 currentVelocity;

    // Makes rerfernce to the player
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    // Follow player
    private void LateUpdate()
    {
        // If player doens't exist then exit the function
        if (!player)
            return;

        transform.position = Vector2.SmoothDamp(transform.position, player.position, ref currentVelocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

}

