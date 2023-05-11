using UnityEngine;



public enum WarriorType
{
    Regular,
    Big,
    Fast,
    Bowman, 
    Gate,
    Castle
}
[System.Serializable]
public class DataUnits 
{
    public int cost;
    public WarriorType warriorType;
    public GameObject warriorPrefab;

}

[System.Serializable]
public class DataEnemyUnits
{
    public int cost;
    public WarriorType warriorType;
}

