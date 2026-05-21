using UnityEngine;

public class WepSMG : MonoBehaviour
{
    [Header("Configuracoes WepSMG")]
    
    [SerializeField] private float intervaloEntreTiros = 0.08f;
    [SerializeField] private int tirosPorRajada = 5;
    [SerializeField] private float cooldownRajada = 0.6f;
    [SerializeField] private float espalhamento = 5f;

    [Header("Referencias")]
    
    [SerializeField] private GameObject prefabTiro;
    [SerializeField] private Transform pontoInicialDoTiro;
    [SerializeField] private AudioClip somDisparo;

    private PlayerController pc;

    private float tempoProximoTiro;
    private int tirosDisparadosNaRajada;
    private bool emCooldownRajada;

    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        Mirar();

        if (Input.GetMouseButton(0))
        {
            ControlarRajada();
        }
        else
        {
            // reseta a rajada se soltar o botão
            tirosDisparadosNaRajada = 0;
            emCooldownRajada = false;
        }
    }

    void ControlarRajada()
    {
        if (Time.time < tempoProximoTiro)
            return;

        // cooldown aps terminar a rajada
        if (emCooldownRajada)
        {
            emCooldownRajada = false;
            tirosDisparadosNaRajada = 0;
        }

        Atirar();

        tirosDisparadosNaRajada++;

        
        if (tirosDisparadosNaRajada >= tirosPorRajada)
        {
            emCooldownRajada = true;
            tempoProximoTiro = Time.time + cooldownRajada;
        }
        else
        {
            
            tempoProximoTiro = Time.time + intervaloEntreTiros;
        }
    }

    void Mirar()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direcao = (mousePos - transform.position).normalized;

        pc.anim.SetFloat("mousePosHorizontal", direcao.x);
        pc.anim.SetFloat("mousePosVertical", direcao.y);
    }

    void Atirar()
    {
        
        float dano = pc.currentStatus.danoRanged / 3f;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direcaoBase =
            (mousePos - pontoInicialDoTiro.position).normalized;

        float desvio = Random.Range(-espalhamento, espalhamento);

        float anguloZ =
            Mathf.Atan2(direcaoBase.y, direcaoBase.x) *
            Mathf.Rad2Deg + desvio;

        Vector2 direcaoFinal = new Vector2(
            Mathf.Cos(anguloZ * Mathf.Deg2Rad),
            Mathf.Sin(anguloZ * Mathf.Deg2Rad)
        );

        GameObject projetil = Instantiate(
            prefabTiro,
            pontoInicialDoTiro.position,
            Quaternion.identity
        );

        projetil.GetComponent<Projetil>()
            .Inicializar(direcaoFinal, dano);

        pc.soundController.TocarSom(somDisparo);
    }
}