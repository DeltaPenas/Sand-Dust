using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    
    [Header("Skills, Ult e Dash")]
    public Transform skillPoint;
    //public SkillBase skillBase;
    public UltBase ultBase;
    public DashBase dashBase;

    [Header("Atributos")]
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

    [Header("Modificações")]
    public PlayerStatus baseStatus;
    public PlayerStatus currentStatus;
    public int gems;
    public List<StatModifier> activeModifiers = new List<StatModifier>();
    private HashSet<System.Type> efeitosRegistrados = new HashSet<System.Type>();
    
    public event Action<GameObject> OnHit;
    public event System.Action<GameObject> OnRicochete;
    public event System.Action<float> OnVidaMaxChanged;
    public event Action OnDash;
    public event Action OnShot;

   
    
    
    private void Awake()
    {
        
        currentStatus = baseStatus.Clone();
        RecalculateStats();
        //AplicarBonusPermanentes();
        
        

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
        rig.linearVelocity = movimento.normalized * currentStatus.velocidade;
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
                if (soundController != null && passosSfx != null)
                {
                    soundController.TocarSom(passosSfx);
                }
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
            case PlayerStatus.StatsType.dashCooldown:
                Apply(ref currentStatus.dashCooldown, mod);
                break;
            case PlayerStatus.StatsType.forcaDash:
                Apply(ref currentStatus.forcaDash, mod);
                break;
            case PlayerStatus.StatsType.ricochetes:
                ApplyInt(ref currentStatus.ricochetes, mod);
                break;
    
        
        }
    }
    public void Apply(ref float stat, StatModifier mod)
    {
    if (mod.ehporcentagem)
        stat *= (1 + mod.valor);
    else
        stat += mod.valor;
    }
    public void ApplyInt(ref int stat,StatModifier mod)
    {
        stat += mod.valorInt;
    }

    public void RecalculateStats()
{
    float vidaAntiga = currentStatus != null
        ? currentStatus.vidaMax
        : baseStatus.vidaMax;

    currentStatus = baseStatus.Clone();

    AplicarBonusPermanentes(); //aqui o outro uso

    foreach (var mod in activeModifiers)
    {
        AplicarModificacao(mod);
    }

    if (currentStatus.vidaMax != vidaAntiga)
    {
        OnVidaMaxChanged?.Invoke(currentStatus.vidaMax);
    }
}

    public void AddModifier(StatModifier mod)
    {
        activeModifiers.Add(mod);
        RecalculateStats();
    }
   public void AddEfeito(EfeitoCarta efeito)
    {
        var tipo = efeito.GetType();

        if (efeitosRegistrados.Contains(tipo))
            return;

        efeitosRegistrados.Add(tipo);

        Debug.Log("Aplicando efeito: " + tipo.Name);

        efeito.Aplicar(this);
    }

    public void DispararOnHit(GameObject alvo)
    {
        OnHit?.Invoke(alvo);
    }
    public void DispararOnRicochete(GameObject alvo)
    {
        OnRicochete?.Invoke(alvo);
    }
    public void DispararOnDash()
    {
        OnDash?.Invoke();
    }
   

    public void AplicarBonusPermanentes()
    {
        if (ProgressionManager.Instance == null) return;

        var pm = ProgressionManager.Instance;

        currentStatus.vidaMax += pm.vidaBonus;
        currentStatus.danoRanged += pm.danoRangedBonus;
        currentStatus.danoMelee += pm.danoMeleeBonus;
        currentStatus.danoSkill += pm.danoSkillBonus;
        currentStatus.danoUlt += pm.danoUltBonus;
        currentStatus.velocidade += pm.velocidadeBonus;
    }



}