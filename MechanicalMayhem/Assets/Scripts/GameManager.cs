using SWAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private GameObject escapeMenu;

    private void Awake()
    {
        escapeMenu = GameObjectExtensionMethods.FindDeactivatedGameObject(gameObject, "Canvas", "EscapeMenu");

        InputManager.INPUT_ACTIONS.Main.PauseScreen.started += PauseScreenStarted;
    }

    private void PauseScreenStarted(InputAction.CallbackContext obj)
    {
        Time.timeScale = Convert.ToSingle(escapeMenu.activeSelf);
        escapeMenu.SetActive(!escapeMenu.activeSelf);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
