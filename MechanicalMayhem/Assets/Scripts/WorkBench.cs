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
    private TMP_Text upgradeButtonText;
    private Button prevSelectedButton;
    private Color affordableColour;
    [SerializeField] private Color unaffordableColour;

    private void Awake()
    {
        upgradeButton = gameObject.FindDeactivatedGameObject("Canvas", "WorkbenchUI").transform.Find("UpgradeButton").GetComponent<Button>();
        upgradeButtonText = upgradeButton.transform.GetChild(0).GetComponent<TMP_Text>();
		affordableColour = upgradeButtonText.color;
        
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
            SetUpgradeData(i, parent, upgradeItem);
		}
    }

    private void SetUpgradeData(int i, Transform parent, UpgradeItem upgradeItem)
	{
		int tempI = i;
        UpgradeData upgradeData = upgradeItem.upgradeDatas[i];
		Transform upgradeParent = parent.Find(upgradeData.name);

		upgradeParent.Find("Header").GetComponent<TMP_Text>().text = upgradeData.header;
        upgradeParent.Find("Cost").GetComponent<TMP_Text>().text = upgradeData.cost + " N&B";

        int sLPP = PlayerPrefs.GetInt(upgradeItem.item + "_" + upgradeData.name, 0);
        if (sLPP >= upgradeData.startLevel)
            upgradeData.startLevel = sLPP;

        string levelTextString = "Lv. " + upgradeData.startLevel +
            (upgradeData.maxLevel == 0 ? "" : "/" + upgradeData.maxLevel);
        TMP_Text levelText = upgradeParent.Find("Level").GetComponent<TMP_Text>();
		levelText.text = levelTextString;

		Transform iconTransform = upgradeParent.Find("Icon");
		iconTransform.GetComponent<Image>().sprite = upgradeData.icon;

        Button selector = upgradeParent.Find("Selector").GetComponent<Button>();
		selector.onClick.AddListener(() =>
		{
            Color color = selector.image.color;
            color.a = 100f / 255f;
            selector.image.color = color;
            if (prevSelectedButton && prevSelectedButton != selector)
            {
                color.a = 0;
                prevSelectedButton.image.color = color;
            }
            prevSelectedButton = selector;
            upgradeButton.onClick.RemoveAllListeners();

			if (!CanUpgrade(upgradeItem.upgradeDatas[tempI]))
			{
				upgradeButtonText.color = unaffordableColour;
				return;
			}

			upgradeButtonText.color = affordableColour;
			upgradeButton.onClick.AddListener(() => BuyOnClick(upgradeItem, tempI, levelText));
		});
	}

    private void BuyOnClick(UpgradeItem item, int i, TMP_Text levelText)
    {
        UpgradeData upgradeData = item.upgradeDatas[i];
        Buy(upgradeData, levelText, item.item);

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

    private bool CanUpgrade(UpgradeData upgradeData)
    {
        bool canAfford = PlayerStats.I.GetNutsAndBolts() >= upgradeData.cost;
        bool notMaxLevel = upgradeData.maxLevel == 0 || upgradeData.startLevel < upgradeData.maxLevel;
        return canAfford && notMaxLevel;
	}

    private void Buy(UpgradeData upgradeData, TMP_Text levelText, string item)
    {
        if (!CanUpgrade(upgradeData))
            return;
		PlayerStats.I.ChangeNutsAndBolts(-upgradeData.cost);
        upgradeData.startLevel++;
		string levelTextString = "Lv. " + upgradeData.startLevel +
			(upgradeData.maxLevel == 0 ? "" : "/" + upgradeData.maxLevel);
        levelText.text = levelTextString;
        PlayerPrefs.SetInt(item + "_" + upgradeData.name, upgradeData.startLevel);
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

	private void OnDisable()
	{
		if (prevSelectedButton)
		{
			Color color = prevSelectedButton.image.color;
			color.a = 0;
			prevSelectedButton.image.color = color;
            prevSelectedButton = null;
		}
		upgradeButton.onClick.RemoveAllListeners();
	}

    public UpgradeItem[] GetUpgradeItems() => upgradeItems;

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

