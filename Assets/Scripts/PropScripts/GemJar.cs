using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GemJar : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;

    void Start()
    {
        
        //if(items.Count == 0) return;
        int index = UnityEngine.Random.Range(0, items.Count);

        GameObject drop = items[index];

        GameObject gameObject = Instantiate(
            drop,
            transform.position,
            quaternion.identity
        );
    }

    

    



}