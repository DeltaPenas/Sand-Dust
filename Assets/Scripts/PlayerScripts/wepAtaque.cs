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
        if (Input.GetMouseButtonDown(0) && taRanged)
        {
            TentarAtacar();
        }

        if (meleeTimer > 0)
        {
            meleeTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && !taRanged)
        {
            AtacarMelee();
            meleeTimer = meleeCooldown;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TrocarArma();
        }
        */
      
    }

    private void TrocarArma()
    {
        if (taRanged)
        {
            taRanged = false;
            rangedWep.SetActive(false);
            meleeWep.SetActive(true);
        }
        else if (!taRanged)
        {
            taRanged = true;
            rangedWep.SetActive(true);
            meleeWep.SetActive(false);
        }
    }

    private void TentarAtacar()
    {
        // --- ADICIONADO A CHECAGEM DE MUNIÇÃO (municaoAtual > 0) ---
        if (Time.time >= tempoProximoTiro && municaoAtual > 0)
        {
            Atacar();
            municaoAtual--; // Gasta uma bala
            Debug.Log("Balas restantes: " + municaoAtual); // Mostra no console
            tempoProximoTiro = Time.time + cooldown;
        }
        else if (municaoAtual <= 0)
        {
            Debug.Log("Sem munição! Aperte R para recarregar.");
        }
    }

    private void Atacar()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direcao = mousePos - pontoInicialDoTiro.position;

        GameObject projetil = Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);
        projetil.GetComponent<Projetil>().definirDireção(direcao);
    }

    private void AtacarMelee()
    {
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
    }
    private void OnDrawGizmosSelected()
    {
        if (pontoInicialDoMelee == null) return;
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pontoInicialDoMelee.position, meleeRange);
        }
    }
}