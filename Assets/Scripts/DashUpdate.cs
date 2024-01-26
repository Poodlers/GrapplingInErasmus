using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUpdate : MonoBehaviour
{
    private float dashCooldown;

    private Slider slider;
    void Start()
    {
        dashCooldown = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().dashCooldown;
        slider = GetComponent<Slider>();
        slider.maxValue = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = slider.value + Time.deltaTime;

    }
}
