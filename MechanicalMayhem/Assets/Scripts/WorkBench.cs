using Mono.CompilerServices.SymbolWriter;
using SWAssets;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Workbench : MonoBehaviour
{

    [SerializeField] private UpgradeItem[] upgradeItems;
    private Button upgradeButton;
    private Color affordableColour;
    [SerializeField] private Color unaffordableColour;

    private void Awake()
    {
        upgradeButton = gameObject.FindDeactivatedGameObject("Canvas", "WorkbenchUI").transform.Find("UpgradeButton").GetComponent<Button>();
        affordableColour = upgradeButton.transform.GetChild(0).GetComponent<TMP_Text>().color;
        foreach (UpgradeItem upgradeItem in upgradeItems)
        {
            SetUpgradeDetails(upgradeItem);
        }
    }

    private void SetUpgradeDetails(UpgradeItem upgradeItem)
    {
        Transform parent = transform.Find(upgradeItem.item);
        parent.Find("TabArea").GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = VariableToHuman(upgradeItem.item);
        parent.Find("Title").GetComponent<TMP_Text>().text = VariableToHuman(upgradeItem.item) + " Upgrades";
        for (int i = 0; i < upgradeItem.upgradeDatas.Length; i++)
		{
			int tempI = i;
			Transform upgradeParent = parent.Find(upgradeItem.upgradeDatas[i].name);

            upgradeParent.Find("Header").GetComponent<TMP_Text>().text = upgradeItem.upgradeDatas[i].header;

			int cost = upgradeItem.upgradeDatas[i].cost;
			upgradeParent.Find("Cost").GetComponent<TMP_Text>().text = cost + " N&B";

			upgradeParent.Find("Level").GetComponent<TMP_Text>().text = 
                "Lv. " + upgradeItem.upgradeDatas[i].startLevel + "/" + upgradeItem.upgradeDatas[i].maxLevel;

            Transform iconTransform = upgradeParent.Find("Icon");
            iconTransform.GetComponent<Image>().sprite = upgradeItem.upgradeDatas[i].icon;
            iconTransform.GetComponent<Button>().onClick.AddListener(() =>
			{
				if (!CanAfford(cost))
				{
					// Say can't afford
					print("Can't afford");
					upgradeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = unaffordableColour;
					return;
				}

				// Say can afford
				print("Can afford");
				upgradeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = affordableColour;
				upgradeButton.onClick.AddListener(() => BuyOnClick(upgradeItem, tempI));
			});
		}
    }

    private void BuyOnClick(UpgradeItem item, int i)
    {
        UpgradeData upgradeData = item.upgradeDatas[i];
        Buy(upgradeData.cost);

        object data = Resources.Load<WeaponData>("ScriptableObjects/Current/" + item.item + "_Current"); ;
        
        if (data == null)
        {
            data = PlayerStats.I;
			data.GetType().GetProperty(upgradeData.name).SetValue(data, upgradeData.incrementAmount);
        }
        else
		{
            FieldInfo info = data.GetType().GetField(ToCamelCase(upgradeData.name));
			info.SetValue(data, (float)info.GetValue(data) + upgradeData.incrementAmount);
		}
    }

    private bool CanAfford(int cost) => Player.I.GetNutsAndBolts() >= cost;

    private void Buy(int cost)
    {
        if (CanAfford(cost)) Player.I.ChangeNutsAndBolts(-cost);
    }

    private string ToCamelCase(string name)
    {
        return name.Replace(name[0], char.ToLower(name[0]));
    }

    private string VariableToHuman(string inputString)
    {
		string final = "";
		foreach (char c in inputString.Trim())
		{
			if (char.IsUpper(c) && final.Length > 0)
				final += " ";

			final += c;
		}
        return final;
    }

    [Serializable]
    public class UpgradeItem
    {
        public string item;
        public UpgradeData[] upgradeDatas = new UpgradeData[3];
    }

    [Serializable]
    public class UpgradeData
	{
		public string name;
		public string header;
		public Sprite icon;
		public int cost;
        public float incrementAmount;
        public int startLevel;
        public int maxLevel;
    }

}

