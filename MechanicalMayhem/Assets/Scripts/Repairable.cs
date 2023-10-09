using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{

    [SerializeField] protected List<GameObject> linkedItems = new();
	[SerializeField] protected Sprite repairedSprite;
    [SerializeField] protected int maxNumberOfUses = 10;
	protected bool repaired = false;
    protected int numberOfUses = 0;

	protected virtual void Update()
	{
        ConstantUpdate();

        if (!repaired)
            return;

        RepairedUpdate();
	}

	protected virtual void ConstantUpdate()
	{

	}

	protected virtual void RepairedUpdate()
	{

	}

	public virtual bool AddItem(GameObject item)
    {
        if (!ItemLinked(item)) return false;

        linkedItems.Remove(item);
        Destroy(item);
        if (linkedItems.Count <= 0)
        {
            // Repaired
            OnRepair();
        }
        return true;
    }

    protected virtual bool ItemLinked(GameObject item)
    {
        if (linkedItems.Count <= 0)
            return false;

        return linkedItems.Contains(item);
    }

    protected virtual void OnRepair()
	{
		transform.Find("Base").GetComponent<SpriteRenderer>().sprite = repairedSprite;
		repaired = true;
	}

    public bool IsRepaired() => repaired;

    public void Use()
    {
        if (numberOfUses >= maxNumberOfUses)
		{
            Destroy(gameObject);
            // TODO: spawn repairable death effect
			return;
		}

        numberOfUses++;
    }

}
