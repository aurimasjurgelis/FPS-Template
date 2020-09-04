using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
}
