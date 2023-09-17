using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player"))
			return;

		int bi = SceneManager.GetActiveScene().buildIndex + 1;
		/*if (SceneManager.GetSceneByBuildIndex(bi).buildIndex < 0)
			return;*/
		SceneManager.LoadSceneAsync(bi);
	}

}
