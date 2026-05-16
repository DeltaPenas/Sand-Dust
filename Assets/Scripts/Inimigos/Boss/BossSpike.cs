using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpike : MonoBehaviour
{
    
    [SerializeField] private FirstBossController boss;
    [SerializeField] private CircleCollider2D collider2D;
    [SerializeField] private PlayerVida playerAtual;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private bool playerDentro;
    [SerializeField] private bool taAtiva;



    void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
        boss = FindAnyObjectByType<FirstBossController>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(AtivarTrap());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDentro = true;
            playerAtual = collision.GetComponent<PlayerVida>();
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
        //animação de subir
        if (playerDentro && playerAtual != null)
        {

            playerAtual.DarDanoPlayer(boss.danoTrap);
            taAtiva = true;
            
            
        }

        yield return new WaitForSeconds(0.5f);
        taAtiva = false;
        //animação de descer
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);


    }


 








}