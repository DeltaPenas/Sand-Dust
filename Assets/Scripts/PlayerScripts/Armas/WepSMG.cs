using UnityEngine;

public class WepSMG : MonoBehaviour
{
    [Header("Configuracoes WepSMG")]
    public float cooldownTiro = 0.2f; 
    public float espalhamento = 5f;
    [SerializeField]private float danoBase;

    [Header("Referencias")]
    public GameObject prefabTiro;
    public Transform pontoInicialDoTiro;
    public AudioClip somDisparo;
    
    private PlayerController pc;
    private float tempoProximoTiro;

    void Start()
    {
        // Busca o PlayerController no pai (objeto 'player')
        pc = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        // Se o objeto estiver ativo, ele segue o mouse
        Mirar();

        // GetMouseButton(0) checa se o botao esta SENDO SEGURADO
        if (Input.GetMouseButton(0) && Time.time >= tempoProximoTiro)
        {
            Atirar();
            tempoProximoTiro = Time.time + cooldownTiro;
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
        danoBase = pc.currentStatus.danoRanged/10; 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direcaoBase = (mousePos - pontoInicialDoTiro.position).normalized;

        float desvio = Random.Range(-espalhamento, espalhamento);
        float anguloZ = Mathf.Atan2(direcaoBase.y, direcaoBase.x) * Mathf.Rad2Deg + desvio;
        Vector2 direcaoFinal = new Vector2(Mathf.Cos(anguloZ * Mathf.Deg2Rad), Mathf.Sin(anguloZ * Mathf.Deg2Rad));

        GameObject projetil = Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);
        
       
        projetil.GetComponent<Projetil>().Inicializar(direcaoFinal, danoBase);
        pc.soundController.TocarSom(somDisparo);
      


    }
}