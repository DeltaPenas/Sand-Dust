using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    
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
    if (!podeMover) return;

        anim.SetFloat("Horizontal", movimento.x);
        anim.SetFloat("Vertical", movimento.y);
        anim.SetFloat("Speed", movimento.magnitude);
        rig.linearVelocity = movimento.normalized * velocidade;
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

}