using System.Collections;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    [Header("Status")]
    public float velocidadeRotacao;
    public float distancia = 1.5f;
    public float dano;
    public float angulo;
    public float raioDeDetacção = 20f;
    public LayerMask layerInimigos;
    [Header("Referencias")]

    public Transform player;
    public SkillWisp skillWisp;
    public GameObject projetilObj;
    [SerializeField] private Collider2D[] resultados = new Collider2D[20];

    [System.Obsolete]
    private void Start()
    {
        skillWisp = FindAnyObjectByType<SkillWisp>();
        Destroy(gameObject, 30f);
        dano = skillWisp.skillDmg;
         StartCoroutine(AutoAtaque());
    }

    private void Update()
    {
        if(player == null) return;

        angulo += velocidadeRotacao * Time.deltaTime;
        
        float rad = angulo * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0
        ) * distancia;

        transform.position = player.position + offset;

    }
    public void DefinirAlvo(Transform alvo)
    {
        player = alvo;
    }

    [System.Obsolete]
    public Transform EncontrarAlvoMaisProximo()
{
    int quantidade = Physics2D.OverlapCircleNonAlloc(
        transform.position,
        raioDeDetacção,
        resultados,
        layerInimigos
    );

    Transform alvoMaisProximo = null;
    float menorDistancia = Mathf.Infinity;

    for (int i = 0; i < quantidade; i++)
    {
        Vector2 diff =
            (Vector2)resultados[i].transform.position -
            (Vector2)transform.position;

        float distancia = diff.sqrMagnitude;

        if (distancia < menorDistancia)
        {
            menorDistancia = distancia;
            alvoMaisProximo = resultados[i].transform;
        }
    }

    return alvoMaisProximo;
}

    [System.Obsolete]
    IEnumerator AutoAtaque()
    {
        while (true)
        {
            Transform alvo = EncontrarAlvoMaisProximo();
            if(alvo != null)
            {
                Atacar(alvo);
            }


             yield return new WaitForSeconds(1f);
        }
    }

    public void Atacar(Transform inimigo)
    {
        Vida alvoVida = inimigo.GetComponent<Vida>();
        Vector2 dirInimigo = (inimigo.position - transform.position);

        if(alvoVida != null)
        {
            GameObject projetil = Instantiate(projetilObj, transform.position, quaternion.identity);
            Projetil proj = projetil.GetComponent<Projetil>();
            if (proj !=null)
            {
                proj.Inicializar(dirInimigo, dano);
            }
        }
    }


}