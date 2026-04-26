using System.Collections;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 2f);
    }
    
    void OnTriggerStay(Collision2D alvo)
    {
        Vida vida = alvo.gameObject.GetComponent<Vida>();

        while(alvo.gameObject.CompareTag("inimigo"))
        {
            StartCoroutine(darDanoDeFogo(vida));
            
        }
    }

    IEnumerator darDanoDeFogo(Vida vida)
    {
                
        yield return new WaitForSeconds (0.02f);
        vida.receberDano(5f);

    }

}
