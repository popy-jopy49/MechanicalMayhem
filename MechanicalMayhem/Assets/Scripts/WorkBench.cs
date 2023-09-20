using TMPro;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeCostText;

    private int upgradeCost = 10;

    private void Start()
    {
        UpdateUpgradeCostText();
    }

    private void UpgradeWorkbench()
    {
        int playerGold = Player.I.GetNutsAndBolts();

        if (playerGold >= upgradeCost)
        {
            Player.I.ChangeNutsAndBolts(-upgradeCost);
            upgradeCost += 10;

            UpdateUpgradeCostText();

            // Perform the workbench upgrade logic here
        }
        else
        {
            Debug.Log("Not enough gold to upgrade the workbench");
        }
    }

    private void UpdateUpgradeCostText()
    {
        upgradeCostText.text = upgradeCost + " N&B";
    }
}

