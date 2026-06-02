using UnityEngine;

public class BumerangueProjetil : MonoBehaviour
{
    [Header("Referencias")]
    public SkillBumerangue skillBumerangue;
    private Rigidbody2D rig;

    [Header("Status")]
    public float dano;
    public float velocidadeInterna = 600f;
    public float velocidade = 7f;
    public float tempoIda = 1f;

    private Vector2 direcao;
    private bool voltando;
    private Transform dono;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        skillBumerangue = FindAnyObjectByType<SkillBumerangue>();
        dono = skillBumerangue.pc.transform;

        Invoke(nameof(IniciarRetorno), tempoIda);
    }

    private void Update()
    {
        transform.Rotate(0, 0, velocidadeInterna * Time.deltaTime);

        if (!voltando)
        {
            rig.linearVelocity = direcao * velocidade;
        }
        else
        {
            Vector2 dirVoltar = ((Vector2)dono.position - rig.position).normalized;
            rig.linearVelocity = dirVoltar * velocidade;
        }
    }

    private void IniciarRetorno()
    {
        voltando = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vida vida = other.GetComponent<Vida>();

        if (vida != null)
        {
            vida.receberDano(dano);
        }

        if (voltando && other.transform == dono)
        {
            Destroy(gameObject);
        }
    }

    
    public void Inicializar(float projdano, Vector2 dir)
    {
        dano = projdano;
        direcao = dir;
    }
}