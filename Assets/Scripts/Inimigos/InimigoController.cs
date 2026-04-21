using System.Collections;
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
        runInfos.inimigosDerrotados +=1;
        runInfos.playerScore +=scoreDoInimigo;
        rigidbody2D.simulated = false;
        StartCoroutine(Piscar());
        
    }
    private IEnumerator Piscar()
    {
        if(anim != null)
        {
            anim.SetTrigger("Morreu");
        }
        yield return new WaitForSeconds(1f);
    
    for (int i = 0; i < 20; i++) 
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
        yield return new WaitForSeconds(0.3f);
    }
    spriteRenderer.enabled = true; 
    }
    
}
