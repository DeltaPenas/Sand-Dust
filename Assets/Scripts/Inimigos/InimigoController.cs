using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
   
   public Vida vidaInimigo;
   public int scoreDoInimigo;
   private SalaController salaOrigem;
   private Rigidbody2D rigidbody2D;
   private RunInfos runInfos;
   private SpriteRenderer spriteRenderer;
   public Animator anim;
   [SerializeField] private List<GameObject> gemas;

   
   
   
   void Start()
    {
        runInfos = FindAnyObjectByType<RunInfos>();
        vidaInimigo = GetComponent<Vida>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    
    }

    public void DefinirSalaOrigem(SalaController sala)
    {
        salaOrigem = sala;
    }

    public void contabilizarPerda()
    {

    
    if (salaOrigem == null)return;
    
    salaOrigem.InimigoDerrotado();
    }
    public void inimigoMorrendo()
    {
        if (runInfos != null)
        {
            runInfos.inimigosDerrotados += 1;
            runInfos.playerScore += scoreDoInimigo;
        }

        if (rigidbody2D != null)
        {
            rigidbody2D.simulated = false;
        }

        StartCoroutine(Piscar());
    }
    private IEnumerator Piscar()
    {
        if (anim != null)
        {
            anim.SetTrigger("Morreu");
        }

        yield return new WaitForSeconds(1f);

        if (spriteRenderer == null)
            yield break;

        for (int i = 0; i < 20; i++) 
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.3f);
        }

        spriteRenderer.enabled = true; 
    }

    public void DroparGem()
    {
        if (gemas.Count ==0) return;

        int indice = UnityEngine.Random.Range(0, gemas.Count);

        GameObject  gema = gemas[indice];

        GameObject prefab = Instantiate(
            gema,
            transform.position,
            Quaternion.identity
        );
    }
    
}
