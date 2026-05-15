using System;
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

    [SerializeField] public float tempoRanged;

   
    private Coroutine rotinaAtual;

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
    [SerializeField] private bool podeAtirar = true;



    


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

       
        // ao começar a fight entra no estadoMelee;
        EntrarEstadoMelee();
    }

    void FixedUpdate()
    {
        if(player == null) return;


        switch (currentState)
        {
            case BossState.Melee:
                ComportamentoMelee();
                break;

            case BossState.Ranged:
                ComportamentoRanged();
                break;

            case BossState.Idle:
                ComportamentoIdle();
                break;
            case BossState.Morreu:
                ComportamentoIdle();
                break;
        }
    }

    
    // metodo centralizado para trocar estado
    public void TrocarEstado(BossState novoEstado)
    {
        currentState = novoEstado;
    }

    public void EntrarEstadoRanged()
    {
        
        if (rotinaAtual != null)
        {
            StopCoroutine(rotinaAtual);
        }

        rotinaAtual = StartCoroutine(RotinaRanged());
    }
    public void EntrarEstadoMelee()
    {
        if(rotinaAtual != null)
        {
            StopCoroutine(rotinaAtual);
        }
        rotinaAtual = StartCoroutine(RotinaMelee());
    }

    void ComportamentoMelee()
    {   
        distancia = Vector2.Distance(rb.position, player.position);

        if (distancia < distanciaParada)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direcao =
            ((Vector2)player.position - rb.position).normalized;

        rb.linearVelocity = direcao * velocidade;
    }

    void ComportamentoRanged()
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

    void ComportamentoMorto()
    {
        rb.linearVelocity = Vector2.zero; 
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

    IEnumerator RotinaRanged()
    {
        vidaBoss.invuneravel = true;
        Debug.Log("Entrou no estado Ranged");

        
        TrocarEstado(BossState.Ranged);

        rb.linearVelocity = Vector2.zero;

        
        yield return new WaitForSeconds(tempoRanged);

        Debug.Log("Voltou para Melee");

        
        TrocarEstado(BossState.Melee);
        vidaBoss.invuneravel = false;

        rotinaAtual = null;
    }

    IEnumerator RotinaMelee()
    {
        
        yield return new WaitForSeconds(1f);

        Debug.Log("Atacou");
    }

    void FicarMorto()
    {
        vidaBoss.invuneravel = true;
    }


    void SpawnarTrap()
    {
        if(currentState == BossState.Ranged) //Impedir de chamar uma trap momento depois de trocar de estado
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
        if(currentState == BossState.Ranged)
        {


            foreach(Transform ponto in pontosDeSpawn)
            {

                yield return new WaitForSeconds(0.5f);
                Debug.Log(ponto.name + " -> " + ponto.position);
                GameObject espinho = Instantiate(prefabTiro, ponto.position, quaternion.identity);
                EspinhoVoador proj = espinho.GetComponent<EspinhoVoador>();
                proj.Inicializar(tempoFlutuando, velocidadeDisparo, velocidadeRotacao);


            }


       
        }


    }


}