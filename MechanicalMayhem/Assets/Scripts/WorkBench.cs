using SWAssets;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Workbench : MonoBehaviour
{

    [SerializeField] private UpgradeItem[] upgradeItems;
    private Button upgradeButton;

    private void Awake()
    {
        upgradeButton = gameObject.FindDeactivatedGameObject("Canvas", "WorkbenchUI").transform.Find("UpgradeButton").GetComponent<Button>();

        foreach (UpgradeItem upgradeItem in upgradeItems)
        {
            SetUpgradeDetails(upgradeItem);
        }
    }

    private void SetUpgradeDetails(UpgradeItem upgradeItem)
    {
        Transform parent = transform.Find(upgradeItem.item);
        for (int i = 0; i < upgradeItem.upgradeDatas.Length; i++)
		{
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
					return;
				}

				// Say can afford
				print("Can afford");
				upgradeButton.onClick.AddListener(() => BuyOnClick(upgradeItem, i - 1));
			});
		}
    }

    private void BuyOnClick(UpgradeItem item, int i)
    {
        print(i);
        print(item.upgradeDatas.Length);
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

    [System.Serializable]
    public class UpgradeItem
    {
        public string item;
        public UpgradeData[] upgradeDatas = new UpgradeData[3];
    }

    [System.Serializable]
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

