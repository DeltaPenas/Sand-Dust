using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    public int dano = 1;
    private Vector2 direcao;

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
