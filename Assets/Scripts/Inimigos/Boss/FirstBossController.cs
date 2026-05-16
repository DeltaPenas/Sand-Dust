using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    [Header("Referencia")]

    [SerializeField] private Transform player;
    [SerializeField] private Transform centroDaSala;
    [SerializeField] private InimigoController ic;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private VidaBoss vidaBoss;

    [SerializeField] public BossState currentState = BossState.Idle;

    [Header("Trap")]

    [SerializeField] public float cooldownTrap;
    [SerializeField] public float danoTrap;
    [SerializeField] public float tempoAtivarTrap;
    [SerializeField] private GameObject espinhoPrefab;

    private bool podeAtacarTrap = true;

    [Header("Disparos")]

    [SerializeField] public float tempoFlutuando;
    [SerializeField] public float velocidadeDisparo;
    [SerializeField] public float velocidadeRotacao;

    [SerializeField] private GameObject prefabTiro;
    [SerializeField] private Transform pontoDeDisparo;

    [SerializeField] public int danoDisparo;
    [SerializeField] public float cooldownDisparo;

    [SerializeField] private List<Transform> pontosDeSpawn = new List<Transform>();

    private bool podeAtirar = true;

    [Header("Movimento")]

    [SerializeField] private float velocidade;
    [SerializeField] private float distanciaParada;
    [SerializeField] private float distancia;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ic = GetComponent<InimigoController>();
        anim = GetComponent<Animator>();
        vidaBoss = GetComponent<VidaBoss>();

        if (player == null)
        {
            GameObject alvo = GameObject.FindGameObjectWithTag("Player");

            if (alvo != null)
            {
                player = alvo.transform;
            }
        }

        
        TrocarEstado(BossState.FaseUm);
    }

    void FixedUpdate()
    {
        if (player == null) return;

        switch (currentState)
        {
            case BossState.FaseUm:
                ComportamentoFaseUm();
                break;
            case BossState.FaseDois:
                ComportamentoFaseDois();
                break;

            case BossState.Idle:
                ComportamentoIdle();
                break;

            case BossState.Morreu:
                ComportamentoIdle();
                break;
        }
    }

    public void TrocarEstado(BossState novoEstado)
    {
        currentState = novoEstado;
    }


    void ComportamentoFaseUm()
    {
        rb.linearVelocity = Vector2.zero;

        if (podeAtacarTrap)
        {
            StartCoroutine(CooldownTrap());
        }

        if (podeAtirar)
        {
            StartCoroutine(CooldownTiro());
        }
    }

    void ComportamentoIdle()
    {
        rb.linearVelocity = Vector2.zero;
    }
    void ComportamentoFaseDois()
    {
        //Debug pra saber que ta na fase dois
        transform.Rotate(0, 0, velocidadeRotacao * Time.deltaTime);

        rb.linearVelocity = Vector2.zero;

        if (podeAtacarTrap)
        {
            StartCoroutine(CooldownTrap());
        }

        if (podeAtirar)
        {
            StartCoroutine(CooldownTiro());
        }
    }


    IEnumerator CooldownTrap()
    {
        podeAtacarTrap = false;

        yield return new WaitForSeconds(cooldownTrap);

        SpawnarTrap();

        podeAtacarTrap = true;
    }

    IEnumerator CooldownTiro()
    {
        podeAtirar = false;

        yield return new WaitForSeconds(cooldownDisparo);

        yield return StartCoroutine(AtirarEspinho());

        podeAtirar = true;
    }

 
    void FicarMorto()
    {
        vidaBoss.invuneravel = true;
    }

    void SpawnarTrap()
    {
        if (currentState == BossState.FaseUm || currentState == BossState.FaseDois)
        {
            Instantiate(
                espinhoPrefab,
                player.position,
                quaternion.identity
            );
        }
    }

    IEnumerator AtirarEspinho()
    {
        if (currentState == BossState.FaseUm || currentState == BossState.FaseDois)
        {
            foreach (Transform ponto in pontosDeSpawn)
            {
                yield return new WaitForSeconds(0.5f);

                Debug.Log(ponto.name + " -> " + ponto.position);

                GameObject espinho = Instantiate(
                    prefabTiro,
                    ponto.position,
                    quaternion.identity
                );

                EspinhoVoador proj = espinho.GetComponent<EspinhoVoador>();

                proj.Inicializar(
                    tempoFlutuando,
                    velocidadeDisparo,
                    velocidadeRotacao
                );
            }
        }
    }
}