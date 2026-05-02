using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> plantas;
    void Start()
    {
        int indice = UnityEngine.Random.Range(0, plantas.Count);

        GameObject prop = plantas[indice];

        GameObject plantinha = Instantiate(
            prop,
            transform.position,
            Quaternion.identity,
            transform
        );
        Destroy(gameObject);
        
    }

    
}
