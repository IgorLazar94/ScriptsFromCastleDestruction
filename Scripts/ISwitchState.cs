using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitchState
{
    void SwitchState<T>() where T : State;




}
