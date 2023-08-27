using UnityEngine;
using UnityEngine.EventSystems;

public class TrafficJamController : MonoBehaviour, IDragHandler
{

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 13x5
        // 1/s = 
        Vector2 mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //float axis = rb.

        Vector2 pos = new(transform.position.x, Mathf.Round(mousePos.y));
        transform.position = pos;
    }

}
