using System.Collections;
using UnityEngine;

public class BombProjetil : MonoBehaviour
{
    public float forcaL = 3;
    public int dano = 5;
    public Vector2 direcao;

    public Rigidbody2D rig;

    public void definirDirecao(Vector2 novaDirecao)
    {
        direcao = novaDirecao.normalized;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        rig.AddForce(direcao * forcaL, ForceMode2D.Impulse);

        StartCoroutine(explodir());
    }

    IEnumerator explodir()
    {
        yield return new WaitForSeconds(0.5f);

        Collider2D[] alvos = Physics2D.OverlapCircleAll(transform.position, 2f);

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
}