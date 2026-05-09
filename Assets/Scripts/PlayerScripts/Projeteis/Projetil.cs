using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;

    private Vector2 direcao;
    
    private PlayerController pc;

    private float vidaProjetil;
    private int ricochetesRestantes;
    public bool podeGerarRicocheteExtra = true;

    private bool jaDisparouRicochete = false; 

    void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        Destroy(gameObject, 10f);
    }

     public void Inicializar(Vector2 dir, PlayerController pc)
    {
        direcao = dir.normalized;

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }



    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)direcao * velocidade * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D alvo)
    {
        
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            if (vida != null)
            {
                float dano = pc.currentStatus.danoRanged;

                vida.receberDano(dano);
                pc.DispararOnHit(alvo.gameObject);

                vidaProjetil -= dano; 
            }

            if (vidaProjetil <= 0)
            {
                Destroy(gameObject);
                return;
            }

            TentarRicochete(alvo);
        }

        
        else if (alvo.CompareTag("Parede"))
        {
            TentarRicochete(alvo);
        }
    }

    void TentarRicochete(Collider2D alvo)
{
    if (podeGerarRicocheteExtra)
    {
        podeGerarRicocheteExtra = false;
        pc.DispararOnRicochete(gameObject);
    }

    if (ricochetesRestantes <= 0)
    {
        Destroy(gameObject);
        return;
    }

    ricochetesRestantes--;

    Vector2 ponto = alvo.ClosestPoint(transform.position);
    Vector2 normal = (transform.position - (Vector3)ponto).normalized;

    direcao = Vector2.Reflect(direcao, normal);

    
}
}