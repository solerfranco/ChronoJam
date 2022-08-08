using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GekoMenu : MonoBehaviour
{
    void Start()
    {
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
}
