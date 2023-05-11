using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class EventBus 
{
    public static Action<GameObject> onCreateNewProjectile;
}
