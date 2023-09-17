using UnityEngine;
using UnityEngine.Audio;
using SWAssets;
using UnityEngine.Rendering;

public class GameAssets : Singleton<GameAssets> {

    [System.Serializable]
    public class EnemyPrefabData
    {
        public GameObject g;
        public float chance;
    }

	[Header("Prefabs")]
	public GameObject MessagePrefab;
	public GameObject BulletPrefab;
	public EnemyPrefabData[] EnemyPrefabs;

    [Header("Post Processing")]
    public VolumeProfile VolumeProfile;

    [Header("Mixers")]
    public AudioMixer MainMixer;

	void Awake()
	{
		RegisterSingleton(this);
	}

	public GameObject GetRandomEnemyPrefab()
	{
        float totalSpawnChance = 0f;
        foreach (EnemyPrefabData enemyPrefabData in EnemyPrefabs)
        {
            totalSpawnChance += enemyPrefabData.chance;
        }

        // Generate a random number between 0 and the total spawn chance
        float randomValue = Random.Range(0f, totalSpawnChance);
        
        // Find the first gameobject that has the chance
        GameObject selectedEnemyPrefab = null;
        foreach (var enemyData in EnemyPrefabs)
        {
            if (randomValue < enemyData.chance)
            {
                selectedEnemyPrefab = enemyData.g;
                break;
            }
            randomValue -= enemyData.chance;
        }

        // Spawn the selected enemy
        if (selectedEnemyPrefab)
        {
            return selectedEnemyPrefab;
        }

        return EnemyPrefabs[0] == null ? EnemyPrefabs[0].g : null;
	}

}
