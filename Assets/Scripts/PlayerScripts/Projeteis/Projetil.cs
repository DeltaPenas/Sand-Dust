using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 150f;
    private Vector2 direcao;
    private WepAtaque wp;


    private void Start()
    {
        wp = FindAnyObjectByType<WepAtaque>();
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
            vida.receberDano(wp.pc.currentStatus.danoRanged);
        }

        if (!alvo.CompareTag("Player") && !alvo.CompareTag("Chão"))
        {
            Destroy(gameObject);
        }
    }
}