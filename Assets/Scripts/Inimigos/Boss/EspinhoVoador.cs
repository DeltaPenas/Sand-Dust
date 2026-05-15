using System.Collections;
using UnityEngine;
public class EspinhoVoador : MonoBehaviour
{
    [SerializeField] private float tempoFlutuando;
    [SerializeField] private float velocidadeDisparo;
    [SerializeField] private float velocidadeRotacao;
    [SerializeField] private SoundController soundController;
    [SerializeField] private  AudioClip som;
    [SerializeField] private FirstBossController boss;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;


    void Start()
    {
        soundController = FindAnyObjectByType<SoundController>();
        rb = GetComponent<Rigidbody2D>();
        boss = FindAnyObjectByType<FirstBossController>();

        GameObject alvo =
            GameObject.FindGameObjectWithTag("Player");

        if(alvo != null)
        {
            player = alvo.transform;
        }

        StartCoroutine(RotinaEspinho());
    }


    private void OnTriggerEnter2D(Collider2D alvo)
    {
        
        PlayerVida vidaPlayer = alvo.GetComponent<PlayerVida>();
        Vida vida = alvo.GetComponent<Vida>();
        

        if (vidaPlayer != null)
        {
            vidaPlayer.DarDanoPlayer(boss.danoDisparo);
        }

        if(vida != null)
        {
            vida.receberDano(boss.danoDisparo);
        }
      



        if (!alvo.CompareTag("inimigo") && !alvo.CompareTag("Chão") && !alvo.CompareTag("ProjetilPlayer") && !alvo.CompareTag("inimigo"))
        {
            Destroy(gameObject);
        }
    }



    void Update()
    {
        
        transform.Rotate(0, 0,
            velocidadeRotacao * Time.deltaTime);
    }

    public void Inicializar(float tempoFlutuandoBoss, float velocidadeDisparoBoss, float velocidadeRotacaoBoss)
    {
        tempoFlutuando = tempoFlutuandoBoss;
        velocidadeDisparo = velocidadeDisparoBoss;
        velocidadeRotacao = velocidadeRotacaoBoss;
    }

    IEnumerator RotinaEspinho()
    {
        
        yield return new WaitForSeconds(tempoFlutuando);

       
        velocidadeRotacao = 0;

       
        Vector2 direcao =
            (player.position - transform.position).normalized;

        float angulo =
            Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, angulo);

        yield return new WaitForSeconds(0.3f);

      
        rb.linearVelocity =
            direcao * velocidadeDisparo;
        soundController.TocarSom(som);
    }
}