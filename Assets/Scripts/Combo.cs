using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public sealed class Combo : MonoBehaviour
{

    public static Combo instance;
    public TextMeshProUGUI textUi;
    private int comboMultiplier;
    public int ComboMultiplier
    {
        get
        {
            return comboMultiplier;
        }
        set
        {
            comboMultiplier = value;
            textUi.text = "x" + value;
        }
    }

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
        if(enemyType == "Fly")
        {
            ComboMultiplier ++;
        }
        if (enemyType == "Bee")
        {
            ComboMultiplier = 0;
        }
    }
}
