using UnityEngine;
using UnityEngine.Audio;
using SWAssets;

public class GameAssets : Singleton<GameAssets> {

	[Header("Prefabs")]
	public GameObject MessagePrefab;
	public GameObject EnemyBulletPrefab;

	void Awake()
	{
		RegisterSingleton(this);
	}

}
