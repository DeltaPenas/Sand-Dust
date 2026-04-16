using UnityEngine;

public class InimigoController : MonoBehaviour
{
   
   private Vida vidaInimigo;
   private SalaController salaOrigem;
   
   void Start()
    {
        vidaInimigo = GetComponent<Vida>();
    }

    public void DefinirSalaOrigem(SalaController sala)
    {
        salaOrigem = sala;
    }

    public void contabilizarPerda()
    {
    if (salaOrigem == null)
    {
        Debug.LogError("Sala origem não definida");
        return;
    }

    salaOrigem.InimigoDerrotado();
    }
}
