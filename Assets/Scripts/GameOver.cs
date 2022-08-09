using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreUi;
    private PlayerInputActions playerInputActions;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Disable();
        LeanTween.moveLocalY(gameObject, 0, 1).setEaseInOutQuad();
        Invoke(nameof(EnableControls), 1f);
        scoreUi.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }

    void EnableControls()
    {
        playerInputActions.UI.Enable();
        playerInputActions.UI.Continue.performed += Restart;
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        playerInputActions.UI.Disable();
        SceneManager.LoadScene(4);
    }
}
