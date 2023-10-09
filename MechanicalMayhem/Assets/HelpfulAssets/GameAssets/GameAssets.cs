using UnityEngine;
using UnityEngine.Audio;
using SWAssets;
using UnityEngine.Rendering;

public class GameAssets : Singleton<GameAssets> {

    [System.Serializable]
    public class PrefabData
    {
        public GameObject g;
        public float chance;
    }

	[Header("Prefabs")]
	public GameObject MessagePrefab;
	public GameObject BulletPrefab;
    public PrefabData[] DronePrefabs;
	public PrefabData[] EnemyPrefabs;

    [Header("Effects")]
	public GameObject DroneExplosionPrefab;
	public GameObject EnemyDeathEffect;

	[Header("Post Processing")]
    public VolumeProfile VolumeProfile;

    [Header("Mixers")]
    public AudioMixer MainMixer;

	void Awake()
	{
		RegisterSingleton(this);
	}

	public GameObject GetRandomPrefab(PrefabData[] prefabDatas)
	{
        float totalSpawnChance = 0f;
        foreach (PrefabData enemyPrefabData in prefabDatas)
        {
            totalSpawnChance += enemyPrefabData.chance;
        }

        // Generate a random number between 0 and the total spawn chance
        float randomValue = Random.Range(0f, totalSpawnChance);
        
        // Find the first gameobject that has the chance
        GameObject selectedEnemyPrefab = null;
        foreach (var enemyData in prefabDatas)
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

        return prefabDatas[0] == null ? prefabDatas[0].g : null;
	}

}
