using UnityEngine;
using UnityEngine.UI;

public class Workbench : MonoBehaviour
{
    public int upgradeLevel = 1;
    public Button upgradeButton;
    public Text upgradeCostText;

    private int upgradeCost = 10;

    private void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeWorkbench);
        UpdateUpgradeCostText();
    }

    private void UpgradeWorkbench()
    {
        int playerGold = Player.I.GetNutsAndBolts(); // Replace this with your own logic to get the player's gold

        if (playerGold >= upgradeCost)
        {
            Player.I.ChangeNutsAndBolts(-upgradeCost);
            upgradeLevel++;
            upgradeCost += 10;

            UpdateUpgradeCostText();

            // Perform the workbench upgrade logic here

            Debug.Log("Workbench upgraded to level " + upgradeLevel);
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

