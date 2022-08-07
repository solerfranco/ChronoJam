using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour

{
    public Slider ageSlider;
    public bool isStarted;
    public TextMeshProUGUI textUi;
    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            textUi.text = ((int)value).ToString();
            ageSlider.value = value;
        }
    }
    public float StartingLife;
    public float EnemyLifeIncrease;

    private float timePlayed;
    public float TimePlayed
    {
        get { return timePlayed; }
        set { timePlayed = value;
            PlayerPrefs.SetInt("Score", (int) value);
        }
    }

    float life;

    void Start()
    {
        Life = StartingLife;
        ageSlider.maxValue = StartingLife;
    }

    void Update()
    {
        if (isStarted)
        {
            TimePlayed += Time.deltaTime;
            Life -= lifeDecreaseRate(TimePlayed);
            Die();
        }
    }

    float lifeDecreaseRate(float timePlayed)
    {
        float decreaseValue = Time.deltaTime;
        if(timePlayed > 30f && timePlayed <= 60f)
        {
           decreaseValue *= 1.2f;
        }
        if (timePlayed > 60f && timePlayed <= 90f)
        {
            decreaseValue *= 1.4f;
        }
        if (timePlayed > 90f)
        {
            decreaseValue *= 1.8f;
        }
        return decreaseValue;
    }

    public void IncreaseLife()
    {
        Life += EnemyLifeIncrease;

        if(Life > StartingLife)
        {
            Life = StartingLife;
        }
    }

    public void DecreaseLife(string enemyType)
    {

        if (enemyType == "Bee")
        {
            Life -= 5;
        }

        if (enemyType == "Bug")
        {
            Life -= 20;
        }
    }


    void Die()
    {
        if(Life <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

}
