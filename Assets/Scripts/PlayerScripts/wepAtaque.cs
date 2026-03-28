using UnityEngine;

public class WepAtaque : MonoBehaviour
{
    public float cooldown = 0.5f;
    private float tempoProximoTiro;
    private float meleeTimer;
    public int meleeDano;
    public float meleeRange = 1;
    public float meleeCooldown;
    public LayerMask layerInimigos;
    public GameObject prefabTiro;
    public GameObject rangedWep;
    public GameObject meleeWep;
    public Transform pontoInicialDoTiro;
    public Transform pontoInicialDoMelee;
    public bool taRanged = true;
    public PlayerController pc;
    public AudioClip fireSoundEffcet;
   

    // --- NOVAS VARIÁVEIS DE MUNIÇÃO ---
    public int municaoMaxima = 10; // Quantidade máxima de balas
    private int municaoAtual;      // Quantidade de balas no momento

    private void Start()
    {
        // Enche a arma quando o jogo começa
        municaoAtual = municaoMaxima;
    }

    private void Update()
    {
        Vector3 mouseatq = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseatq.z = 0f;
        Vector2 direcaoatq = mouseatq - pontoInicialDoTiro.position;
        pc.anim.SetFloat("mousePosHorizontal",direcaoatq.x);
        pc.anim.SetFloat("mousePosVertical", direcaoatq.y);
        

        if (Input.GetMouseButtonDown(0))
        {
            
            TentarAtacar();
        }

        if (meleeTimer > 0)
        {
            meleeTimer -= Time.deltaTime;
            //// meleeTimer = meleeCooldown;
        }

        if (Input.GetMouseButtonDown(1) && pc.movimento.magnitude == 0)
        {
            
            AtacarMelee();
            meleeTimer = meleeCooldown;
        }
    }

    private void TentarAtacar()
    {
        
        if (Time.time >= tempoProximoTiro)
        {
            Atacar();
            tempoProximoTiro = Time.time + cooldown;
        }
    }

    private void Atacar()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direcao = mousePos - pontoInicialDoTiro.position;
    

        GameObject projetil = Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);
        projetil.GetComponent<Projetil>().definirDireção(direcao);
        AudioSource.PlayClipAtPoint(fireSoundEffcet, transform.position);
    }

    private void AtacarMelee()

    {
        rangedWep.SetActive(false);
        Collider2D[] hitAlvos = Physics2D.OverlapCircleAll(
            pontoInicialDoMelee.position,
            meleeRange,
            layerInimigos
        );

        foreach (Collider2D alvos in hitAlvos)
        {
            Vida vida = alvos.GetComponent<Vida>();
            if (vida != null)
            {
                vida.receberDano(meleeDano);
            }
        }
        pc.anim.SetTrigger("attack"); 
        Invoke("AtivarArma", 0.6f);
    }
    private void OnDrawGizmosSelected()
    {
        
        if (pontoInicialDoMelee == null) return;
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pontoInicialDoMelee.position, meleeRange);
        }
    }

    public void AtivarArma()
    {
        rangedWep.SetActive(true);
    }
    
}