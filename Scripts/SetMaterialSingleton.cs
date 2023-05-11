using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MaterialsType
{
    Wood,
    Stone,
    Brick
}

public class SetMaterialSingleton : SingletonBase<SetMaterialSingleton>
{
    [SerializeField] private List<MaterialType> _materialTypes;
    [SerializeField] private Material _defoultMaterial;
    public Material getMaterial(MaterialsType _type)
    {
        foreach (var value in _materialTypes)
        {
            if (value.TypeMaterial == _type)
            {
                _defoultMaterial = value.Material;
            }
        }

        return _defoultMaterial;
    }

    public int getHP(MaterialsType _type)
    {
        int defoultHP = 0;

        foreach (var value in _materialTypes)
        {
            if (value.TypeMaterial == _type)
            {
                defoultHP = value.MaterialHP;
            }
        }

        if(defoultHP <= 0)
        {
            Debug.LogWarning("Not correct hp in partBuild");
        }
        return defoultHP;
    }
}

[Serializable]

public class MaterialType
{
    public Material Material;
    public MaterialsType TypeMaterial;

    public int MaterialHP;
}
