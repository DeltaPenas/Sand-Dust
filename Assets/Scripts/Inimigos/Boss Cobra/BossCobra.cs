using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCobra : InimigoBase
{
    [Header("Referencias")]
    [SerializeField] private VidaBossCobra vidaBossCobra;
    [SerializeField] private List<GameObject> segmentos;
    [SerializeField] private List<Transform> posPonto;
    [SerializeField] private Transform pontoAtual;
    [SerializeField] private int refPontoAtual = 0;
    [SerializeField] public BossState currentState = BossState.Idle;
    [SerializeField] private GameObject bolaDeFogo;
    [SerializeField] private float velocidadeProjetil = 7f;

    [Header("Status")]

    public float tempoEntreAtaques = 3f;
    public int dano = 2;
    public int danoBolaDeFogo = 1;
    private float proximoAtaque;

    public float velocidadeBoss = 1f;
    public float velocidadeMovimento;

    protected override void Start()
    {
        vidaBossCobra = GetComponent<VidaBossCobra>();
        pontoAtual = posPonto[0];
        base.Start();
        TrocarEstado(BossState.Idle);

        proximoAtaque = Time.time + 0.5f;

        velocidadeMovimento = velocidadeBoss;
    }

    private void Update()
    {
        
        Mover();
        Comportamento();

        if(Vector2.Distance(transform.position, pontoAtual.position) < 0.2f)
        {
            DefinirProximoLocal();
        }
    }

     void OnCollisionEnter2D(Collision2D alvo)
    {
        if (alvo.gameObject.CompareTag("Player") && currentState != BossState.Idle && currentState != BossState.Morreu)
        {
            PlayerVida playerVida = alvo.gameObject.GetComponent<PlayerVida>();

            playerVida.DarDanoPlayer(dano);
        }
    }



    protected override void Comportamento()
    {
        if (player == null) return;

        
        if(vida.morreu) return;

        if (Time.time >= proximoAtaque)
        {
            EscolherAtaque();

            proximoAtaque = Time.time + tempoEntreAtaques;
        }
    }

    private void Mover()
{
    if (currentState == BossState.Idle || currentState == BossState.Morreu) return;

    Vector2 direcao = (pontoAtual.position - transform.position).normalized;

    transform.position += (Vector3)direcao * velocidadeMovimento * Time.deltaTime;

    float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

    transform.rotation = Quaternion.Euler(0, 0, angulo);

    if (Vector2.Distance(transform.position, pontoAtual.position) < 0.2f)
    {
        DefinirProximoLocal();
    }
}


    public void DefinirProximoLocal()
    {
       refPontoAtual++;
       velocidadeMovimento = velocidadeBoss;
       
        if(refPontoAtual >= posPonto.Count)
        {
            refPontoAtual = 0;
            velocidadeMovimento *=6;
            
        } 
       pontoAtual = posPonto[refPontoAtual];
    }

  
    public void TrocarEstado(BossState novoEstado)
    {
        currentState = novoEstado;
    }

    void EscolherAtaque()
    {
        int ataque = UnityEngine.Random.Range(0, 2);
        if(currentState == BossState.Idle || currentState == BossState.Morreu) return;

        switch (ataque)
        {
            case 0:
                AtaqueBolasDeFogo();
                break;

            case 1:
                Debug.Log("ataque 2");
                break;
        }
    }

    void AtaqueBolasDeFogo()
    {
        Vector2 direcao = (player.position - transform.position).normalized;

        Vector2 spawnPos = (Vector2)transform.position + direcao * 0.6f;

        GameObject tiro = Instantiate(bolaDeFogo, spawnPos, Quaternion.identity);

        Collider2D colProjetil = tiro.GetComponent<Collider2D>();
        Collider2D colInimigo = GetComponent<Collider2D>();

        if (colProjetil != null && colInimigo != null)
        {
            Physics2D.IgnoreCollision(colProjetil, colInimigo);
        }

        ProjetilInimgioExplosivo proj = tiro.GetComponent<ProjetilInimgioExplosivo>();

        if (proj != null)
        {
            proj.Inicializar(direcao, velocidadeProjetil, (int)(dano+RunManager.Instance.currentRun.inimigoBuffDano));
        }
        Debug.Log("Atirou");
    }

    public void EntrarNaFaseDois()
    {
        TrocarEstado(BossState.FaseDois);
        velocidadeBoss *=2;
        tempoEntreAtaques /=2;
    }

    public void IniciarBoss()
    {
        
        TrocarEstado(BossState.FaseUm);
        SoundController.instance.PlayBossMusic();

    }

    
}