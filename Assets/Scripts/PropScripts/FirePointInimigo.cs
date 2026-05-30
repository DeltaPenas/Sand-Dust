using System.Collections;
using UnityEngine;

public class FirePointInimigo : MonoBehaviour
{
    private PlayerController pc;
    public float dano = 1;
    void Start()
    {
        
        Destroy(gameObject, 2f);
        
    }
    
    void OnTriggerEnter2D(Collider2D alvo)
    {
        
        if (alvo.CompareTag("Player"))
        {
            PlayerVida vida = alvo.GetComponent<PlayerVida>();

            vida.DarDanoPlayer(dano);
        }

    

}
}