using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelLoad : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player"))
			return;

		int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
		/*if (SceneManager.GetSceneByBuildIndex(bi).buildIndex < 0)
			return;*/
		SceneFader.I.FadeToScene(buildIndex);
	}

}
