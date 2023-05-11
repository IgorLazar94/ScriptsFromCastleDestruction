using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class PlayerEnergyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    private float score = 50;

    public static Action<int> OnAddEnergy;
    public static Action<int> OnDecreaseEnergy;

    [SerializeField] Button playerButton;


    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        OnAddEnergy += SetEnergy; //підписуємось на екшен, який додає енергії
        OnDecreaseEnergy += DecreaseEnergy; //підписуємось на екшен, який віднімає енергію
    }

    private void OnDisable()
    {
        OnAddEnergy -= SetEnergy; //підписуємось на екшен, який додає енергії
        OnDecreaseEnergy -= DecreaseEnergy; //підписуємось на екшен, який віднімає енергію
    }
    private void Start()
    {
        _textScore = GetComponent<TextMeshProUGUI>();
        _textScore.text = score.ToString();
        if (score > 0) playerButton.enabled = true;
        
        
    }

    private void SetEnergy(int energy)
    {
        
        score += energy;
        _textScore.text = score.ToString();
        if (score > 0)
        {
            playerButton.enabled = true;
        }
    }

    private void DecreaseEnergy(int energy)
    {
        if (score>0)
        {
            score -= energy;
            _textScore.text = score.ToString();
        }
        else
        {
            playerButton.enabled = false;
        }
            
    }
}
