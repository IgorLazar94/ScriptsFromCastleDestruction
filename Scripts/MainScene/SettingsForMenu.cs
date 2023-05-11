using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsForMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    

    //private 

    void Start()
    {
        menuPanel.SetActive(false);

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettings()
    {
        menuPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        menuPanel.SetActive(false);
        
    }


}
