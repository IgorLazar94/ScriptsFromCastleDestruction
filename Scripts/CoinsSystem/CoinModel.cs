using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinModel : MonoBehaviour
{
    private int coinsAmount;
    private int playerLevel = 2;

    public void IncreaseCoinsAmount(int coins)
    {
        if (PlayerPrefs.HasKey("CoinsAmount"))
        {
            coinsAmount = PlayerPrefs.GetInt("CoinsAmount");
            coinsAmount += coins;
            PlayerPrefs.SetInt("CoinsAmount", coinsAmount);
            PlayerPrefs.Save();
        }
        else
        {
            coinsAmount += coins;
            PlayerPrefs.SetInt("CoinsAmount", coinsAmount);
            PlayerPrefs.Save();
        }
    }

    public void DecreaseCoinsAmount(int coins)
    {
        if (PlayerPrefs.HasKey("CoinsAmount")) // && PlayerPrefs.HasKey("CoinsAmount") > coins
        {
            coinsAmount = PlayerPrefs.GetInt("CoinsAmount");
            coinsAmount -= coins;
            PlayerPrefs.SetInt("CoinsAmount", coinsAmount);
            PlayerPrefs.Save();
        }
        else
        {
            coinsAmount -= coins;
            PlayerPrefs.SetInt("CoinsAmount", coinsAmount);
            PlayerPrefs.Save();
        }
    }

    public void IncreasePlayerLevel()
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            playerLevel = PlayerPrefs.GetInt("PlayerLevel");
            playerLevel += 1;
            PlayerPrefs.SetInt("PlayerLevel", playerLevel);
            PlayerPrefs.Save();
        }
        else
        {
            playerLevel += 1;
            PlayerPrefs.SetInt("PlayerLevel", playerLevel);
            PlayerPrefs.Save();
        }
        
    }

    public int GetCoinAmount()
    {
        if (PlayerPrefs.HasKey("CoinsAmount"))
        {
            return PlayerPrefs.GetInt("CoinsAmount");
        }
        else
        {
            Debug.Log("GetCoinAmount ELSE  === " + coinsAmount);
            return coinsAmount;
        }
        
    }

    public int GetLevel()
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            return PlayerPrefs.GetInt("PlayerLevel");
        }
        else
        {
            PlayerPrefs.SetInt("PlayerLevel", playerLevel);
            PlayerPrefs.Save();
            return PlayerPrefs.GetInt("PlayerLevel");
        }
    }
}
