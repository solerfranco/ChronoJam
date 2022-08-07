using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCounter : MonoBehaviour

{
    public bool IsStarted { get; set; }
    public float Life { get; set; }
    public float StartingLife;
    public float EnemyLifeIncrease;

    private float timePlayed;

    float life;

    void Start()
    {
        Life = StartingLife;
    }

    void Update()
    {
        if (IsStarted)
        {
            timePlayed += Time.deltaTime;
            Life -= lifeDecreaseRate(timePlayed);
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
