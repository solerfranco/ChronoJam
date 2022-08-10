using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour

{
    public static LifeCounter instance;
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
            life = Mathf.Clamp(value, 0, StartingLife);
            textUi.text = ((int)value).ToString();
            ageSlider.value = value;
            if (life <= 0) Die();
        }
    }
    public float StartingLife;

    private float timePlayed;
    public float TimePlayed
    {
        get { return timePlayed; }
        set { timePlayed = value;
            PlayerPrefs.SetInt("Score", (int) value);
        }
    }

    float life;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

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
            Life -= LifeDecreaseRate(TimePlayed);
        }
    }

    float LifeDecreaseRate(float timePlayed)
    {
        float decreaseValue = Time.deltaTime;
        if (timePlayed > 30f && timePlayed <= 60f)
        {
           decreaseValue *= 1.4f;
        }
        if (timePlayed > 60f && timePlayed <= 90f)
        {
            decreaseValue *= 1.6f;
        }
        if (timePlayed > 90f)
        {
            decreaseValue *= 1.8f;
        }
        if(timePlayed > 120f)
        {
            decreaseValue *= 2.1f;
        }
        return decreaseValue;
    }


    void Die()
    {
        SceneManager.LoadScene(1);
    }

}
