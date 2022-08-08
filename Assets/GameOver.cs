using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private bool restart = false;
    public TextMeshProUGUI scoreUi;

    void Start()
    {
        LeanTween.moveLocalY(gameObject, 0, 1).setEaseInOutQuad();
        Invoke(nameof(AllowRestart), 1f);
        scoreUi.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }

    void AllowRestart()
    {
        restart = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D)) && restart)
        {
            SceneManager.LoadScene(4);
        }
    }
}
