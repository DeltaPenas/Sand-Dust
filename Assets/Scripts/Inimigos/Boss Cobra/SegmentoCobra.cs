using System;
using UnityEngine;

public class SegmentoCobra : MonoBehaviour
{
    public Transform alvo;
    public int dano = 1;
    public float velocidadeFollow = 15f;
    public float distancia = 0.5f;
    [SerializeField] private BossCobra bossCobra;


    void Start()
    {
        bossCobra = GetComponent<BossCobra>();
    }
    void Update()
    {
        if (alvo == null) return;

        Vector3 direcao = alvo.position - transform.position;

        if (direcao.magnitude > distancia)
        {
            transform.position += direcao.normalized *
                                  velocidadeFollow *
                                  Time.deltaTime;
        }

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void OnCollisionEnter2D(Collision2D alvo)
    {
        if (alvo.gameObject.CompareTag("Player"))
        {
            PlayerVida playerVida = alvo.gameObject.GetComponent<PlayerVida>();

            playerVida.DarDanoPlayer(dano);
        }
    }
}