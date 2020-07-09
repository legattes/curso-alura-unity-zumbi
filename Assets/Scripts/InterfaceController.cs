using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    private PlayerController playerController;
    public Slider LifeSlider;
    
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        LifeSlider.maxValue = playerController.Life;
        UpdateLifeSlider();
    }
    
    void Update()
    {
    }

    public void UpdateLifeSlider()
    {
        LifeSlider.value = playerController.Life;
    }
}
