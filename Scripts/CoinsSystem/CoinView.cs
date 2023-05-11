using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinView : MonoBehaviour
{
    public TMP_Text coinsAmount;
    public TMP_Text level;

    public void UpdateCoinsAmount(int coins)
    {
        if (PlayerPrefs.HasKey("CoinsAmount"))
        {
            coinsAmount.text = PlayerPrefs.GetInt("CoinsAmount").ToString();
            Debug.Log("UpdateCoinsAmount IF === " + coinsAmount.text);
        }
        else
        {
            coinsAmount.text = coins.ToString();
            Debug.Log("UpdateCoinsAmount ELSE === " + coinsAmount.text);
        }
        
    }

    public void UpdatePlayerLevel(int baseLevel)
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            level.text = "Level: " + PlayerPrefs.GetInt("PlayerLevel");
        }
        else
        {
            level.text = "Level: " + baseLevel;
        }
    }
}
