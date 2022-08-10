using UnityEngine;
using TMPro;

public sealed class Combo : MonoBehaviour
{

    public static Combo instance;
    public TextMeshProUGUI textUi;
    private int comboMultiplier = 0;
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
            textUi.enabled = value >= 1;
            switch (value)
            {
                case 1:
                    textUi.color = Color.white;
                    break;
                case 2:
                    textUi.color = Color.green;
                    break;
                case 3:
                    textUi.color = Color.yellow;
                    break;
                case 4:
                    textUi.color = Color.magenta;
                    break;
                case 5:
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
}
