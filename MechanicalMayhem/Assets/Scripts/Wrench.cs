using SWAssets.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wrench : MonoBehaviour
{

    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatToHit;
    private float time;
    private bool isFiring;

    private void Start()
    {
        InputManager.INPUT_ACTIONS.Main.Fire.started += FireStarted;
        InputManager.INPUT_ACTIONS.Main.Fire.canceled += FireCanceled;
    }

    private void Update()
    {
        if (time < 1 / fireRate)
        {
            time += Time.deltaTime;
            return;
        }

        if (!isFiring)
            return;

        time = 0;
        
        Vector3 mousePos = InputManager.INPUT_ACTIONS.Main.MousePosition.ReadValue<Vector2>();
        mousePos.z = 0.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos = mousePos - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.parent.parent.position, mousePos, range, whatToHit);
        print(hit);
        print(hit.transform);
        if (!hit.transform)
            return;

        Attackable attackable = hit.transform.GetComponent<Attackable>();
        if (!attackable)
        {
            // Spawn wall hit decal
            return;
        }

        attackable.Damage(damage);
        // Spawn hit affect
    }

    private void FireStarted(InputAction.CallbackContext obj)
    {
        isFiring = true;
    }

    private void FireCanceled(InputAction.CallbackContext obj)
    {
        isFiring = false;
    }

}
