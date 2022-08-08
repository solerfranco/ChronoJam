using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class IntroductionScript : MonoBehaviour
{
    public Image earth;
    public Image earthDestroyed;
    public Image shadowGecko;
    public Image lightGecko;
    public Image worldBurns;
    public Button button;
    public TextMeshProUGUI textUi;

    public void Transition(Image image, Color from, Color to)
    {
        StartCoroutine(ChangeColor(image, from, to));
    }

    private IEnumerator ChangeColor(Image image,Color from, Color to)
    {
        float lerpAmount = 0;
        while (lerpAmount < 1)
        {
            lerpAmount += 0.02f;
            image.color = Color.Lerp(from, to, lerpAmount);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void WorldDestroyed()
    {
        Transition(earthDestroyed, Color.clear, Color.white);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ShadowGecko);
        textUi.text = "The dark demon Rigoberto appeared, wounded and looking for someone to possess";
    }

    public void ShadowGecko()
    {
        Transition(earthDestroyed, Color.white, Color.black);
        Transition(shadowGecko, Color.clear, Color.white);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LightGecko);
        textUi.text = "And so, he found the perfect subject";
    }

    public void LightGecko()
    {
        lightGecko.enabled = true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(WorldBurns);
        textUi.text = "You!";
    }

    public void WorldBurns()
    {
        Transition(lightGecko, Color.white, Color.black);
        Transition(worldBurns, Color.clear, Color.white);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LoadGame);
        textUi.text = "You, Pedro, now must eat to survive, without food you'll grow older in just seconds!";
        textUi.color = Color.white;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(4);
        button.onClick.RemoveAllListeners();
    }
}
