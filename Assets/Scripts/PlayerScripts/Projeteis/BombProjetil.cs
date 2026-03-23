using System.Collections;
using UnityEngine;

public class BombProjetil : MonoBehaviour
{
    public float forcaL = 3;
    public int dano = 5;
    public Vector2 direcao;
    public Vector2 posBala;
    public Transform balaPos;
    public Rigidbody2D rig;
    public SkillBomb bmb;
    public float raioExp;
    public GameObject explosionVFX;
    

    public void definirDirecao(Vector2 novaDirecao)
    {
        direcao = novaDirecao.normalized;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        rig.AddForce(direcao * forcaL, ForceMode2D.Impulse);

        StartCoroutine(explodir());
    }

    private IEnumerator explodir()
    {
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

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioExp);
    }
}