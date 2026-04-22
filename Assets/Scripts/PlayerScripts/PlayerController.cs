using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public PlayerStatus baseStatus;
    public PlayerStatus currentStatus;
    [Header("Skills, Ult e Dash")]
    public SkillBase skillBase;
    public UltBase ultBase;
    public DashBase dashBase;
    [Header("Atributos")]
    public int vida;
    public float velocidade;
    public Vector2 ultimadireção;
    public float iframetempo = 0.3F;
    public float iframeTempoBuff = 0;
    public bool podeMover = true;
    public Vector2 movimento;
    public Rigidbody2D rig;
    public BoxCollider2D boxCollider2D;
    public Animator anim;
    public AudioClip passosSfx;
    
    public bool iframeAtivo = false;
    public SoundController soundController;
    private PlayerVida pv;
    public bool emTeleporte;
  
    private void Start()
    {
        currentStatus = baseStatus.Clone();

        pv = FindAnyObjectByType<PlayerVida>();
        soundController = FindAnyObjectByType<SoundController>();
        rig = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(TocarPassos());
    }

    private void Update()
    {

        movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        if (movimento != Vector2.zero)
        {
            ultimadireção = movimento.normalized;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashBase.tentaUsarDash();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            skillBase.tentaUsar();
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ultBase.tentaUsar();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pv.DarDanoPlayer(1);
        }

    }

    private void FixedUpdate()
    {
        mover();
    }

    private void mover()
    {
    if (podeMover)
        {
        anim.SetFloat("Horizontal", movimento.x);
        anim.SetFloat("Vertical", movimento.y);
        anim.SetFloat("Speed", movimento.magnitude);
        rig.linearVelocity = movimento.normalized * velocidade;
        }
        else
        {
            movimento = Vector2.zero;
        }

        
    }

    IEnumerator TocarPassos()
    {
        while (true)
        {
            if (movimento.magnitude > 0.01f)
            {
                soundController.TocarSom(passosSfx);
            }
            yield return new WaitForSeconds(0.40f);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("inimigo") && iframeAtivo)
    {
        Debug.Log("não colidiu");
    }

    if (collision.gameObject.CompareTag("inimigo") && !iframeAtivo)
    {
        Debug.Log("colidiu");
    }
}
    
    public void AplicarModificacao(StatModifier mod)
    {
        switch (mod.stat)
        {
            case PlayerStatus.StatsType.vidaMax:
                Apply(ref currentStatus.vidaMax, mod);
                break;
            case PlayerStatus.StatsType.velocidade:
                Apply(ref currentStatus.velocidade, mod);
                break;
            case PlayerStatus.StatsType.danoMelee:
                Apply(ref currentStatus.danoMelee, mod);
                break;
            case PlayerStatus.StatsType.danoRanged:
                Apply(ref currentStatus.danoRanged, mod);
                break;
            case PlayerStatus.StatsType.danoSkill:
                Apply(ref currentStatus.danoSkill, mod);
                break;
            case PlayerStatus.StatsType.rangeSkill:
                Apply(ref currentStatus.rangeSkill, mod);
                break;
            case PlayerStatus.StatsType.cooldownSkill:
                Apply(ref currentStatus.cooldownSkill, mod);
                break;
            case PlayerStatus.StatsType.danoUlt:
                Apply(ref currentStatus.danoUlt, mod);
                break;
            case PlayerStatus.StatsType.rangeUlt:
                Apply(ref currentStatus.rangeUlt, mod);
                break;
            case PlayerStatus.StatsType.cooldownUlt:
                Apply(ref currentStatus.cooldownUlt, mod);
                break;
            case PlayerStatus.StatsType.atqCooldown:
                Apply(ref currentStatus.atqCooldown, mod);
                break;
    
        
        }
    }
    void Apply(ref float stat, StatModifier mod)
    {
    if (mod.taAtivo)
        stat *= (1 + mod.valor);
    else
        stat += mod.valor;
    }


}