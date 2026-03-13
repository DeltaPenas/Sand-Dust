using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 150f;
    public int dano = 1;
    private Vector2 direcao;

    // --- NOVA FUNÇÃO START ---
    void Start()
    {
        // Destrói este objeto automaticamente após 3 segundos para não lotar a memória
        Destroy(gameObject, 3f); 
    }

    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D alvo)
    {
        Vida vida = alvo.GetComponent<Vida>();

        if (vida != null)
        {
            vida.receberDano(dano);
        }

        if (!alvo.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}