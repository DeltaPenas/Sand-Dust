using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MallItemSpawner: MonoBehaviour
{
    
    [SerializeField] private List<GameObject> items;



    void Start()
    {
        SortearItem();
    }

    void SortearItem()
    {
        if(items.Count == 0) return;
        int indice = UnityEngine.Random.Range(0, items.Count);

        GameObject item = items[indice];

        GameObject itemEscolhido = Instantiate(
            item,
            transform.position,
            quaternion.identity
            
        );

        Destroy(gameObject);


    }




} 