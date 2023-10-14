using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{

    [SerializeField] protected List<GameObject> linkedItems = new();
	[SerializeField] protected Sprite repairedSprite;
    [SerializeField] protected int maxNumberOfUses = 10;
    [SerializeField] protected Puzzle puzzle;
	protected bool repaired = false;
    protected int numberOfUses = 0;
    protected bool inPuzzle = false;

    private Transform puzzlesParent;

    protected enum Puzzle
	{
		None,
		Maze,
        RushHour,
        ImagePuzzle,
    }

    protected virtual void Awake()
    {
        puzzlesParent = Camera.main.transform.Find("Puzzles");
	}

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
        bool itemAdded = false;
        if (ItemLinked(item))
		{
			linkedItems.Remove(item);
			Destroy(item);
            itemAdded = true;
		}
        if (ReadyToBeRepaired())
        {
			// Repaired
			AllItemsCollected();
        }
        return itemAdded;
    }

    protected virtual bool ItemLinked(GameObject item) => linkedItems.Contains(item);

    protected virtual void AllItemsCollected()
    {
        if (GameManager.I.InPuzzle())
            return;

        switch (puzzle)
		{
			default:
			case Puzzle.None:
				OnRepair();
                return;
            case Puzzle.Maze:
                PuzzleGrid.Setup(GameAssets.I.MazePuzzle, OnRepair);
                break;
            case Puzzle.RushHour:
                Instantiate(GameAssets.I.RushHourPuzzle, puzzlesParent).Find("PuzzleWin").GetComponent<PuzzleWin>().SetWinFunc(OnRepair);
                break;
            case Puzzle.ImagePuzzle:
                PuzzleGrid.Setup(GameAssets.I.ImagePuzzle, OnRepair);
                break;
        }

        GameManager.I.SetInPuzzle(true);
        inPuzzle = true;
    }

    public void ExitPuzzle(bool removeRepairable)
	{
        if (!inPuzzle)
            return;

		inPuzzle = false;
		GameManager.I.SetInPuzzle(false);
		if (puzzlesParent.childCount > 0) Destroy(puzzlesParent.GetChild(0).gameObject);
        if (removeRepairable) GameObject.Find("Player").GetComponent<Player>().RemoveRepairable(this);
	}

    protected void OnRepair()
	{
		repaired = true;
        ExitPuzzle(true);
        OnRepairGFX();
    }

    protected virtual void OnRepairGFX()
	{
		transform.Find("Base").GetComponent<SpriteRenderer>().sprite = repairedSprite;
	}

    public bool ThisPuzzle() => inPuzzle;

    public bool IsRepaired() => repaired;
    public bool ReadyToBeRepaired() => linkedItems.Count <= 0;

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
