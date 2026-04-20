using System;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public DungeonGeneratortest dg;
    void Start()
    {
        dg = FindAnyObjectByType<DungeonGeneratortest>();
    }

    public void SpawnarProp()
    {
        if(dg.catalogoProps.props.Count ==0) return;

        int indice = UnityEngine.Random.Range(0, dg.catalogoProps.props.Count);

        GameObject prefab = dg.catalogoProps.props[indice];

        GameObject prop = Instantiate(
            prefab,
            transform.position,
            Quaternion.identity,
            transform
        );
    }
}
