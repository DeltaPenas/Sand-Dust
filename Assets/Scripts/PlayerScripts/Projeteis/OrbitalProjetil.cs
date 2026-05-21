using UnityEngine;

public class OrbitalProjetil : MonoBehaviour
{
    public float velocidadeRotacao = 300f;
    public float distancia = 1.5f;
    public float rotacaoInterna = 900f;
    public float dano;
    public float duracao = 10f;
    private float angulo;
    public Transform player;
    public UltOrbital ultOrbital;

    
    private void Start()
    {
       ultOrbital = FindAnyObjectByType<UltOrbital>(); 
       Destroy(gameObject, ultOrbital.ultCooldown/2); 
       dano = ultOrbital.ultDmg;
       
    }
    
    private void Update()
    {
        if (player == null) return;

        angulo += velocidadeRotacao * Time.deltaTime;

        float rad = angulo * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0
        ) * distancia;

        transform.position = player.position + offset;

        transform.Rotate(0, 0, rotacaoInterna * Time.deltaTime);
    }

    public void DefinirPlayer(Transform alvo)
    {
        player = alvo;
    }

    private void OnTriggerEnter2D(Collider2D alvo)
    {
        if (alvo.CompareTag("inimigo"))
        {
            Vida vida = alvo.GetComponent<Vida>();

            if (vida != null)
            {

                vida.receberDano(dano);

            }
        }
    }
}