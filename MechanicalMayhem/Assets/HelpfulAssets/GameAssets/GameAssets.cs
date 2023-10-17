﻿using UnityEngine;
using UnityEngine.Audio;
using SWAssets;
using UnityEngine.Rendering;

public class GameAssets : Singleton<GameAssets> {

    [System.Serializable]
    public class PrefabData<T>
    {
        public T obj;
        public float chance;
    }

	[Header("Prefabs")]
	public GameObject MessagePrefab;
	public GameObject BulletPrefab;
    public PrefabData<GameObject>[] DronePrefabs;
	public PrefabData<GameObject>[] EnemyPrefabs;
	public PrefabData<string>[] MazeFiles;
	public PrefabData<string>[] ImageFiles;
	public PrefabData<string>[] ImageComparisonFiles;
    public PrefabData<Transform>[] RushHourPuzzles;
    public Transform ImagePuzzle;
    public Transform MazePuzzle;

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

	public T GetRandomPrefab<T>(PrefabData<T>[] prefabDatas)
	{
        float totalSpawnChance = 0f;
        foreach (PrefabData<T> enemyPrefabData in prefabDatas)
        {
            totalSpawnChance += enemyPrefabData.chance;
        }

        // Generate a random number between 0 and the total spawn chance
        float randomValue = Random.Range(0f, totalSpawnChance);
        
        // Find the first gameobject that has the chance
        T selectedEnemyPrefab = default;
        foreach (PrefabData<T> enemyData in prefabDatas)
        {
            if (randomValue < enemyData.chance)
            {
                selectedEnemyPrefab = enemyData.obj;
                break;
            }
            randomValue -= enemyData.chance;
        }

		return selectedEnemyPrefab;
	}

}
