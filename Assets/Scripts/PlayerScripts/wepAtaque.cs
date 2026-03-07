using UnityEngine;

public class wepAtaque : MonoBehaviour
{
    public float cooldown = 0.5f;
    private float tempoProximoTiro;
    private float meleeTimer;
    public int meleeDano;
    public float meleeRange = 1;
    public float meleeCooldown;
    public LayerMask layerInimigos;
    public GameObject prefabTiro;
    public Transform pontoInicialDoTiro;
    public Transform pontoInicialDoMelee;



    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TentarAtacar();
        }

        if (meleeTimer > 0)
        {
            meleeRange -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            AtacarMelee();
            meleeTimer = meleeCooldown;
        }
    }

    void TentarAtacar()
    {
        if (Time.time >= tempoProximoTiro)
        {
            Atacar();
            tempoProximoTiro = Time.time + cooldown;
        }
    }

    void Atacar()
    {
        Debug.Log("atirou");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direcao = mousePos - pontoInicialDoTiro.position;

        GameObject projetil = Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);
        projetil.GetComponent<Projetil>().definirDireção(direcao);
    }
    void AtacarMelee()
    {
        Collider2D[] hitAlvos = Physics2D.OverlapCircleAll(
            pontoInicialDoMelee.position,
            meleeRange,
            layerInimigos
        );

        foreach (Collider2D alvos in hitAlvos)
        {
            Debug.Log("acertou o " + alvos.name);
            Vida vida = alvos.GetComponent<Vida>();

            if (vida != null)
            {
                vida.receberDano(meleeDano);
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if (pontoInicialDoMelee == null) return;
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pontoInicialDoMelee.position, meleeRange);
        }
    }
}
