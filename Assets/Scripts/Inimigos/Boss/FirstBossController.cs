using System.Collections;
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

    // ALTERAÇÃO:
    // Corrigido nome currentSate -> currentState
    [SerializeField] public BossState currentState = BossState.Idle;

    [SerializeField] public float tempoRanged;

    // ALTERAÇÃO:
    // Guarda a coroutine atual do estado
    private Coroutine rotinaAtual;

    [Header("Trap")]

    [SerializeField] private float cooldownTrap;
    [SerializeField] public float danoTrap;
    [SerializeField] public float tempoAtivarTrap;
    [SerializeField] private GameObject espinhoPrefab;

    private bool podeAtacarTrap = true;

    [Header("Movimento")]

    [SerializeField] private float velocidade;
    [SerializeField] private float distanciaParada;
    [SerializeField] private float distancia;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ic = GetComponent<InimigoController>();
        anim = GetComponent<Animator>();

        if (player == null)
        {
            GameObject alvo = GameObject.FindGameObjectWithTag("Player");

            if (alvo != null)
            {
                player = alvo.transform;
            }
        }

       
        // ao começar a fight entra no estadoRanged
        EntrarEstadoRanged();
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
        }
    }

    
    // metodo centralizado para trocar estado
    void TrocarEstado(BossState novoEstado)
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
    }

    void ComportamentoIdle()
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

   

    IEnumerator RotinaRanged()
    {
        Debug.Log("Entrou no estado Ranged");

        
        TrocarEstado(BossState.Ranged);

        rb.linearVelocity = Vector2.zero;

        
        yield return new WaitForSeconds(tempoRanged);

        Debug.Log("Voltou para Melee");

        
        TrocarEstado(BossState.Melee);

        rotinaAtual = null;
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
}