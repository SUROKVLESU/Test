using System;
using UnityEngine;

[Serializable]
public class Resource
{
    [SerializeField] private Resources TypeResource;
    [SerializeField] private int CountResource;
    public Resource(Resources resource,int count)
    {
        TypeResource = resource;
        CountResource = count;
    }
    public int GetCount() { return CountResource; }
    public Resources GetTypeResource() { return TypeResource; }
}

