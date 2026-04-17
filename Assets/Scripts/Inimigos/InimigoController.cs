using UnityEngine;

public class InimigoController : MonoBehaviour
{
   
   private Vida vidaInimigo;
   private SalaController salaOrigem;
   private Rigidbody2D rigidbody2D;
   
   
   
   void Start()
    {
        vidaInimigo = GetComponent<Vida>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    
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
        rigidbody2D.simulated = false;
    }
}
