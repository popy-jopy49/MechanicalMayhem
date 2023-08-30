using UnityEngine;
using UnityEngine.EventSystems;

public class TrafficJamController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] private bool y = false;
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool targetCar = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 13x5
        // 1/s = 
        Vector2 mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 pos = mousePos - rb.position;
        if (y)
        {
            float axis = mousePos.y;
            pos.x = 0;
            //pos = new(0, Mathf.Round(axis));
        }
        else
        {
            float axis = mousePos.x;
            pos.y = 0;
            //pos = new(Mathf.Round(axis), 0);
        }
        rb.velocity = speed * Time.deltaTime * pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rb.isKinematic = false;
    }

    public bool GetTargetCar() => targetCar;

}
