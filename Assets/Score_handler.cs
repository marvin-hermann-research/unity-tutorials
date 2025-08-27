using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // einbauen der libary


public class Score_handler : MonoBehaviour
{
    public int CoinScore = 0;
    public TMP_Text ScoreText;


    void Update()
    {
        ScoreText.text = ""+ CoinScore;
    }

    public void FoundCoin()
    {
        CoinScore = CoinScore + 1;
    }
}
