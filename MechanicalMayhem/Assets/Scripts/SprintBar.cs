using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{

    private Slider bar;

    private void Awake()
    {
        bar = GetComponent<Slider>();
        TDPlayerMovement.OnSprintChanged += OnSprintChanged;
    }

    private void OnSprintChanged(float sprint, float maxSprint)
    {
        bar.value = sprint / maxSprint;
    }

}
