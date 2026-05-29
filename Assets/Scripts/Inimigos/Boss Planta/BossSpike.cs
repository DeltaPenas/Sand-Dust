using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpike : MonoBehaviour
{
    
    [SerializeField] private FirstBossController boss;
    [SerializeField] private BoxCollider2D collider2D;
    [SerializeField] private PlayerVida playerAtual;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;

    [SerializeField] private bool playerDentro;
    [SerializeField] private bool taAtiva;



    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        boss = FindAnyObjectByType<FirstBossController>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine(AtivarTrap());
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (!taAtiva) return;

    if (collision.CompareTag("Player"))
    {
        PlayerVida player = collision.GetComponent<PlayerVida>();

        if (player != null)
        {
            player.DarDanoPlayer(boss.danoTrap);
        }
    }
}

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerDentro )
        {
            playerDentro = false;
            playerAtual = null;
        }
    }

    



        IEnumerator AtivarTrap()
    {
        yield return new WaitForSeconds(boss.tempoAtivarTrap);

        taAtiva = true;
        collider2D.enabled = true;

        anim.SetTrigger("Up");

        yield return new WaitForSeconds(0.5f);

        taAtiva = false;
        collider2D.enabled = false;

        anim.SetTrigger("Down");

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    








}