using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{

    private Slider bar;
    private Transform arrow;

    // Gets reference to bar and arrow
    private void Start()
    {
        bar = GetComponent<Slider>();
        arrow = transform.Find("MinSprint").Find("Arrow");
        TDPlayerMovement.OnSprintChanged += OnSprintChanged;
    }

    // Do some math to find the correct position taking into account the min and max sprint
    private void OnSprintChanged(float sprint, float maxSprint, float minSprint)
    {
        if (!arrow)
            return;

        Vector2 pos = arrow.localPosition;
        pos.y = (minSprint / maxSprint * 310) - 155;
        arrow.localPosition = pos;
        bar.value = sprint / maxSprint;
    }

}
