using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{

    private Slider bar;
    private Transform arrow;

    private void Awake()
    {
        bar = GetComponent<Slider>();
        TDPlayerMovement.OnSprintChanged += OnSprintChanged;

        arrow = transform.Find("MinSprint").Find("Arrow");
    }

    private void OnSprintChanged(float sprint, float maxSprint, float minSprint)
    {
        Vector2 pos = arrow.localPosition;
        pos.y = (minSprint / maxSprint * 310) - 155;
        arrow.localPosition = pos;
        bar.value = sprint / maxSprint;
    }

}
