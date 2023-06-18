using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{

    protected List<GameObject> linkedItems = new List<GameObject>();
    protected bool repaired = false;
    [SerializeField] protected Sprite repairedSprite;

    public virtual bool AddItem(GameObject item)
    {
        if (linkedItems.Contains(item))
        {
            linkedItems.Remove(item);
            if (linkedItems.Count <= 0)
            {
                // Repaired
                transform.Find("GFX").GetComponent<SpriteRenderer>().sprite = repairedSprite;
                repaired = true;
            }
        }
        return false;
    }

}
