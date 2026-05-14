using System.Collections;
using UnityEngine;

public class BombProjetil : MonoBehaviour
{
    private float forcaL = 5;
    private float velocidadeGiro = 630f;
    private float dano;
    public Vector2 direcao;
    public Vector2 posBala;
    public Transform balaPos;
    public Rigidbody2D rig;
    public SkillBomb bmb;
    public float raioExp;
    public GameObject explosionVFX;
    public AudioClip explosionSFX;
    private PlayerController pc;
    

    public void definirDirecao(Vector2 novaDirecao)
    {
        direcao = novaDirecao.normalized;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        pc = FindAnyObjectByType<PlayerController>();
        dano = pc.currentStatus.danoSkill;
        raioExp = pc.currentStatus.rangeSkill;

        rig.AddForce(direcao * forcaL, ForceMode2D.Impulse);

        StartCoroutine(explodir());
        SoundController soundController = FindAnyObjectByType<SoundController>();
    }
    private void Update()
    {
        transform.Rotate(0, 0, velocidadeGiro * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D alvo)
    {
       
        if (!alvo.CompareTag("Player") && !alvo.CompareTag("Chão"))
        {
        
        SoundController soundController = FindAnyObjectByType<SoundController>();
        GameObject exp = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        SpriteRenderer sr = exp.GetComponent<SpriteRenderer>();
        float tamanhoAtual = sr.bounds.size.x;
        float escala = (raioExp * 0.5f)  / tamanhoAtual;

        exp.transform.localScale = Vector3.one * escala;

        Collider2D[] alvos = Physics2D.OverlapCircleAll(transform.position, raioExp);

        foreach (Collider2D objAlvo in alvos)
        {
            Vida vida = objAlvo.GetComponent<Vida>();

            if (vida != null)
            {
                vida.receberDano(dano);
            }
        }
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.1f);
        soundController.TocarSom(explosionSFX);
        Destroy(gameObject);
        }
    }

    private IEnumerator explodir()
    {
        SoundController soundController = FindObjectOfType<SoundController>();
        yield return new WaitForSeconds(0.5f);

        GameObject exp = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        SpriteRenderer sr = exp.GetComponent<SpriteRenderer>();
        float tamanhoAtual = sr.bounds.size.x;
        float escala = (raioExp * 0.5f)  / tamanhoAtual;

        exp.transform.localScale = Vector3.one * escala;

        Collider2D[] alvos = Physics2D.OverlapCircleAll(transform.position, raioExp);

        foreach (Collider2D alvo in alvos)
        {
            Vida vida = alvo.GetComponent<Vida>();

            if (vida != null)
            {
                vida.receberDano(dano);
            }
        }
        
        soundController.TocarSom(explosionSFX);
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.1f);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioExp);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            explodir();
        }
    }
}

    