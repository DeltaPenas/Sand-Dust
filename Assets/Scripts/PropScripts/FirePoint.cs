using System.Collections;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    private PlayerController pc;
    public float danoDeFogoInicial;
    void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        danoDeFogoInicial = pc.currentStatus.danoRanged;
        Destroy(gameObject, 6f);
        
    }
    
    void OnTriggerEnter2D(Collider2D alvo)
    {
        
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            vida.PegarFogo(danoDeFogoInicial, 5);

            
        }

    

}
}