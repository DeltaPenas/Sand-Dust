using UnityEngine;

public class HealPoint : MonoBehaviour
{
    private PlayerVida pv;
    [SerializeField]private AudioClip som;





    void OnTriggerEnter2D(Collider2D alvo)
    {
        if (alvo.CompareTag("Player"))
        {
            Debug.Log("encostou");
            
            pv = FindAnyObjectByType<PlayerVida>();
            if(pv == null) return;

            if(pv.playerVidaAtual < pv.playerVidaTotal)
            {
                pv.CurarPlayer(1);
                SoundController.instance.TocarSom(som);
                Destroy(gameObject);
                
            }


        }
    }



}