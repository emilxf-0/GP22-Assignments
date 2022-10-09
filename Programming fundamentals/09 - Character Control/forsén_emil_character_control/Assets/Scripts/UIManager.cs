using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider slider;
    
    private void Start()
    {
        scoreText.text = "Score " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score " + playerScore;
    }

    public void ChargeJump(float chargeAmount)
    {
        slider.value = chargeAmount;
    }
    
    

}