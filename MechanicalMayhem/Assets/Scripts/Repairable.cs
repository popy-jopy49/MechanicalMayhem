using System;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{

    [SerializeField] protected List<GameObject> linkedItems = new List<GameObject>();
	[SerializeField] protected Sprite repairedSprite;
	protected bool repaired = false;

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
        if (linkedItems.Count <= 0)
            return false;

        if (linkedItems.Contains(item))
        {
            linkedItems.Remove(item);
            Destroy(item);
            if (linkedItems.Count <= 0)
            {
                // Repaired
                OnRepair();
            }
            return true;
        }
        return false;
    }

    protected virtual void OnRepair()
	{
		transform.Find("GFX").GetComponent<SpriteRenderer>().sprite = repairedSprite;
		repaired = true;
	}

}
