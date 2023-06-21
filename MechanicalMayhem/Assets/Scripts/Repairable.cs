using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{

    [SerializeField] protected List<GameObject> linkedItems = new List<GameObject>();
	[SerializeField] protected Sprite repairedSprite;
	protected bool repaired = false;

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
                transform.Find("GFX").GetComponent<SpriteRenderer>().sprite = repairedSprite;
                repaired = true;
            }
            return true;
        }
        return false;
    }

}
