using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControll : MonoBehaviour
{
    public static CoinControll instance;
    public CoinModel coinModel;
    public CoinView coinView;



    private void Start()
    {
        Debug.Log("START COIN AMOUNT == " + coinModel.GetCoinAmount());
        if (instance == null)
        {
            instance = this;
     
            coinView.UpdateCoinsAmount(coinModel.GetCoinAmount());
            coinView.UpdatePlayerLevel(coinModel.GetLevel());
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void OnPlayerGetCoins(int coins)
    {
        coinModel.IncreaseCoinsAmount(coins);
        coinView.UpdateCoinsAmount(coinModel.GetCoinAmount());
    }

    public void OnPlayerSpendCoins(int coins)
    {
        coinModel.DecreaseCoinsAmount(coins);
        coinView.UpdateCoinsAmount(coinModel.GetCoinAmount());
    }

    public void OnPlayerLevelUp()
    {
        coinModel.IncreasePlayerLevel();
        coinView.UpdatePlayerLevel(coinModel.GetLevel());
    }

}
