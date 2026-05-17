
using UnityEngine;

public class UltPickup : MonoBehaviour
{
    public GameObject ultPrefab;



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            UltManager um = collision.GetComponent<UltManager>();

            um.EquiparUlt(ultPrefab);

            Destroy(gameObject);
        }    
    }


}