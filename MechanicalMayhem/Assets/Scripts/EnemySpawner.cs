using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private int numberPerSpawn = 1;
    private float time;

    private void Update()
    {
        if (time < timeBetweenSpawns)
        {
            time += Time.deltaTime;
            return;
        }

        for (int i = 0; i < numberPerSpawn; i++)
        {
            GameObject enemyPrefab = GameAssets.I.GetRandomPrefab(GameAssets.I.EnemyPrefabs);
            if (!enemyPrefab)
                break;

            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }

        time = 0;
    }

}
