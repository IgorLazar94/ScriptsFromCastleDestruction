using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            levelText.text = "Current level: " + PlayerPrefs.GetInt("PlayerLevel");
        }
        else
        {
            PlayerPrefs.SetInt("PlayerLevel", 1);
            levelText.text = "Current level: " + PlayerPrefs.GetInt("PlayerLevel");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
