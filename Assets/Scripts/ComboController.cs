using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{

    public int ComboMultiplier { get; set; }

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
