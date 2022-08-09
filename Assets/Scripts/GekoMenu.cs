using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GekoMenu : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
        playerInputActions.UI.Continue.performed += Continue;
        Invoke(nameof(Animation), 0.5f);
    }

    public void Rickroll()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=GKNB7Eid-ek&ab_channel=NimbusFilms");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Animation()
    {
        LeanTween.moveLocalY(gameObject, -40, 1f).setEaseOutBounce();
    }

    private void Continue(InputAction.CallbackContext obj)
    {
        playerInputActions.UI.Disable();
        LoadGame();
    }
}
