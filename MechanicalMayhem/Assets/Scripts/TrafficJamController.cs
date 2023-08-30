using UnityEngine;
using UnityEngine.EventSystems;

public class TrafficJamController : MonoBehaviour, IDragHandler
{

    [SerializeField] private bool y = false;
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
        Vector2 pos = new();
        if (y)
        {
            print("y");
            float axis = mousePos.y;
            pos = new(transform.position.x, Mathf.Round(axis));
            print(Mathf.Round(axis));
        }
        else
        {
            print("x");
            float axis = mousePos.x;
            pos = new(Mathf.Round(axis), transform.position.y);
        }
        transform.position = pos;
        print(pos.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop dragging?
    }

}
