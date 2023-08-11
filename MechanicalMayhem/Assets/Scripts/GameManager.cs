using SWAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private GameObject escapeMenu;

    private void Start()
    {
        escapeMenu = GameObjectExtensionMethods.FindDeactivatedGameObject(gameObject, "Canvas", "EscapeMenu");

        InputManager.Initialise();
        InputManager.INPUT_ACTIONS.Main.PauseScreen.started += PauseScreenStarted;
    }

    private void PauseScreenStarted(InputAction.CallbackContext obj)
    {
        Time.timeScale = Convert.ToSingle(escapeMenu.activeSelf);
        escapeMenu.SetActive(!escapeMenu.activeSelf);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
