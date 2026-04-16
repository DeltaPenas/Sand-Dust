using System;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public CatalogoProps catalogoProps;
    void Start()
    {
        
    }

    public void SpawnarProp()
    {
        if(catalogoProps.props.Count ==0) return;

        int indice = UnityEngine.Random.Range(0, catalogoProps.props.Count);

        GameObject prefab = catalogoProps.props[indice];

        GameObject prop = Instantiate(
            prefab,
            transform.position,
            Quaternion.identity
        );
    }
}
