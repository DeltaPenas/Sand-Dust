using System.Collections;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 2f);
    }
    
    void OnTriggerEnter2D(Collider2D alvo)
    {
        
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            
        }

    

}
}