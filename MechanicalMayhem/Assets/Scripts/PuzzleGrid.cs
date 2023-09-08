using UnityEngine;
using System.IO;

public class PuzzleGrid : MonoBehaviour
{

    private GridObject[,] grid;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private Vector2 gridObjectSize;
    [SerializeField] private Transform wallPrefab;
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Transform winPrefab;

    private void Awake()
    {
        grid = new GridObject[(int)gridSize.x, (int)gridSize.y];
        string path = "Assets/Resources/MazeBinary.txt";
        StreamReader reader = new(path);
        string text = reader.ReadToEnd().Trim().Replace("\n", "").Replace("\r", "");
        reader.Close();
        int dataIndex = 0;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                char digit = text[dataIndex];
                //Vector2 pos = (new Vector2(y, -x) * gridObjectSize) + (((gridSize * gridObjectSize)+gridObjectSize) / 2f) - gridObjectSize;
                Vector2 pos = (new Vector2(y, -x) * gridObjectSize);
                grid[x, y] = new GridObject(digit, pos, gridObjectSize, transform.parent.Find("InnerWalls"), wallPrefab, playerPrefab, winPrefab);
                print(digit);
                print(dataIndex);
                dataIndex++;
            }
        }
    }

    private class GridObject
    {

        public GridObject(char digit, Vector2 pos, Vector2 size, Transform parent, Transform wallPrefab, Transform playerPrefab, Transform winPrefab)
        {
            Transform prefab = null;
            switch (digit)
            {
                case '0':
                    prefab = wallPrefab;
                    break;
                case 'P':
                    prefab = playerPrefab;
                    break;
                case 'X':
                    prefab = winPrefab;
                    break;
                case '1':
                default:
                    break;
            }
            if (!prefab)
                return;

            Transform p = Instantiate(prefab, pos, Quaternion.identity, parent);
            p.localScale = size;
        }

    }

}
