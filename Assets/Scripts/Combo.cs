using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Combo : MonoBehaviour
{

    private static Combo instance;

    public int ComboMultiplier { get; set; }

    public static Combo GetInstance()
    {
        if (instance == null)
        {
            instance = new Combo();
        }
        return instance;
    }

    void Start()
    {
        ComboMultiplier = 0;
    }

    public void ResetCombo() {
        ComboMultiplier = 0;
    }

    public void IncrementCombo()
    {
        ComboMultiplier++;
    }

    //Cambiar dependiendo de tipo de enemigo
    public void IncrementComboByEnemyType(string enemyType)
    {
        if(enemyType == "enemy")
        {
            ComboMultiplier+=2;
        }
    }
}
