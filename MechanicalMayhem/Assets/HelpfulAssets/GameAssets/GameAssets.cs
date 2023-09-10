﻿using UnityEngine;
using UnityEngine.Audio;
using SWAssets;

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
	public Transform Puzzle_WallPrefab;
	public Transform Puzzle_PlayerPrefab;
	public Transform Puzzle_WinPrefab;
	public EnemyPrefabData[] EnemyPrefabs;

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
