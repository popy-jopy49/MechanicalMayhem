using UnityEngine;
using UnityEngine.Audio;
using SWAssets;

public class GameAssets : Singleton<GameAssets> {

	[Header("Prefabs")]
	public GameObject MessagePrefab;
	public GameObject ShardPrefab;
	public GameObject IndicatorPrefab;

	void Awake()
	{
		RegisterSingleton(this);
	}

}
