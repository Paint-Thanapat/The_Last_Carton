using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptHealthBar : MonoBehaviour
{
    private Slider slider;
    //public Gradient gradient;
    private Image fill;
    void Awake()
    {
        slider = GetComponent<Slider>();
        fill = GameObject.Find("Fill").GetComponent<Image>();
    }

    public void SetMaxHealth(int HP)
    {
        slider.maxValue = HP;
        slider.value = HP;
       // fill.color = gradient.Evaluate(1f);
    }

    public void Sethealth(int HP)
    {
        slider.value = HP;
       // fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
