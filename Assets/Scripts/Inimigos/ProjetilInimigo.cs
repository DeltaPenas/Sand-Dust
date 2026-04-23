using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    private Vector2 direcao;
    private InimigoAtirador ia;
    


    private void Start()
    {
        ia = FindAnyObjectByType<InimigoAtirador>();
        Destroy(gameObject, 3f); 
    }

    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    private void Update()
    {
        transform.Translate(direcao * ia.velocidadeProjetil * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D alvo)
    {
        PlayerVida vida = alvo.GetComponent<PlayerVida>();

        if (vida != null)
        {
            vida.DarDanoPlayer(ia.dano);
        }

        if (!alvo.CompareTag("inimigo") && !alvo.CompareTag("Chão"))
        {
            Destroy(gameObject);
        }
    }
}