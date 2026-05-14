using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class FirstBossController : MonoBehaviour 
{

    [Header("Referencia")]

    
    [SerializeField] private Transform player;
    [SerializeField] private Transform centroDaSala;
    [SerializeField] private InimigoController ic;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] public BossState currentSate = BossState.Idle;

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

    [Header("Desvio Local")] 
    [SerializeField] private float tempoDesvio = 0.7f;
    [SerializeField] private LayerMask layerObstaculos;
    [SerializeField] private float fimDesvio;
    [SerializeField] private Vector2 direcaoDesvioAtual; 

    
    


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
        
        currentSate = BossState.Ranged;
        
    }
    void FixedUpdate()
    {
        if(player == null) return;


        switch (currentSate)
        {
            case BossState.Melee:
            ComporamentoMelee();
            break;

        case BossState.Ranged:
            ComporamentoRanged();
            break;

        case BossState.Idle:
            ComportamentoIdle();
            break;
        }


       



            
            
        
    }

    void ComporamentoMelee()
    {   
        distancia = Vector2.Distance(rb.position, player.position);

        if (distancia < distanciaParada)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direcao =((Vector2)player.position - rb.position).normalized;

        rb.linearVelocity = direcao * velocidade;

    }

    void ComporamentoRanged()
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


    void SpawnarTrap()
    {
        GameObject trap = Instantiate(
            espinhoPrefab,
            player.position,
            quaternion.identity

        );
    }













}