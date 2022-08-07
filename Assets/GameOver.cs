using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private bool restart = false;

    void Start()
    {
        LeanTween.moveLocalY(gameObject, 0, 1).setEaseInOutQuad();
        Invoke(nameof(AllowRestart), 1f);
    }

    void AllowRestart()
    {
        restart = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D)) && restart)
        {
            SceneManager.LoadScene(0);
        }
    }
}
