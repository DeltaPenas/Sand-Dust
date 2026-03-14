using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 150f;
    public int dano = 1;
    private Vector2 direcao;


    private void Start()
    {
        
        Destroy(gameObject, 3f); 
    }

    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    private void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D alvo)
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