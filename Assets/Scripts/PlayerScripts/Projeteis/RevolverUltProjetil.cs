using UnityEngine;

public class RevolverUltProjetil : MonoBehaviour
{   
    public UltRevolver ult;
    public float velocidade;
    private Vector2 direcao;

    public void definirDireção(Vector2 novaDireção)
    {
        direcao = novaDireção.normalized;
    }

    private void Update()
    {
        transform.position += (Vector3)(direcao * velocidade * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D alvo)
    {
        Vida vida = alvo.GetComponent<Vida>();

        if (vida != null)
        {
            vida.receberDano(5);
        }

        if (alvo.CompareTag("inimigo"))
        {
            Destroy(gameObject);
        }
    }



}
