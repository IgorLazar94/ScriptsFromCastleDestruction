using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonUnitBase<T> : MonoBehaviour where T : MonoBehaviour
{
    /*private static T _instance;
    public T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + "SingletonUnitBase";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }*/


}
