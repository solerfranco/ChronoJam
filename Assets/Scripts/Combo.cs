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
            switch (value)
            {
                case 0:
                    textUi.color = Color.white;
                    break;
                case 1:
                    textUi.color = Color.green;
                    break;
                case 2:
                    textUi.color = Color.yellow;
                    break;
                case 3:
                    textUi.color = Color.magenta;
                    break;
                case 4:
                    textUi.color = Color.red;
                    break;
                default:
                    textUi.color = Color.cyan;
                    break;
            }
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
