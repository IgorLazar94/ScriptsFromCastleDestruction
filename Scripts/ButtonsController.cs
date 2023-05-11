using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] SpownUnitsManager spown;
    [SerializeField] WarriorType warrior;
    // Start is called before the first frame update
    
    public void SpownPlayerWarrior()
    {

    }

    public void SpownRegularPWarriorButton()
    {
        spown.SpownRegularPWarrior();
    }
    public void SpownFastPWarriorButton()
    {
        spown.SpownFastPWarrior();
    }
    public void SpownBigPWarriorButton()
    {
        spown.SpownBigPWarrior();
    }
    public void SpownBowmanPWarriorButton()
    {
        spown.SpownBowmanPWarrior();
    }
}
