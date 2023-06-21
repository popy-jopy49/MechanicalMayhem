using UnityEngine;
using UnityEngine.Audio;
using SWAssets;

public class GameAssets : Singleton<GameAssets> {

	[Header("Prefabs")]
	public GameObject MessagePrefab;

	void Awake()
	{
		RegisterSingleton(this);
	}

}
