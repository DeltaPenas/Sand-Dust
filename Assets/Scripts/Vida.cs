using System.Collections;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaTotal;
    public float vidaAtual;
    public bool morreu;
    public bool invuneravel;
    public bool pegandoFogo = false;
    private SpriteRenderer sr;
    [SerializeField] protected DanoVisual dv; 
    [SerializeField] protected float danoAcumulado;
    [SerializeField] protected float ultimoDanoRecebido;

    public int xpDrop = 1;

    
    

    private InimigoController ic;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ic = GetComponent<InimigoController>();
        dv = GetComponent<DanoVisual>();
        
        vidaTotal += RunManager.Instance.currentRun.inimigoLifeBuff;
        
        vidaAtual = vidaTotal;
    }
    public void PegarFogo(float dano, int ticks)
    {
        float danoDeFogo = dano/2;
        if (!pegandoFogo)
        {
            pegandoFogo = true;
            StartCoroutine(ReceberDanoPeloTempo());
            



        }
        IEnumerator ReceberDanoPeloTempo()
        {
            for (int i = 0; i < ticks; i++)
            {
                yield return new WaitForSeconds(1f);

                receberDano(danoDeFogo);
            }

            if (pegandoFogo)
            {
                pegandoFogo = false;
            }
            
        }
        
    }

    public void receberDano(float dano)
    {
        if (morreu)
        return;


        if (!invuneravel)
        {
            ultimoDanoRecebido = dano;
            vidaAtual -= dano;
            danoAcumulado += dano;

        AoReceberDano();
             if (dv != null)
            {
                dv.TomouDano();
            }

        }
        
    if (vidaAtual <= 0)
    {
        morreu = true;

        

        if (ic != null)
        {
            if (RunManager.Instance != null)
            {
                RunManager.Instance.AddXp(ic.scoreDoInimigo); // XP ganho nesta run, para UI/resumo
                RunManager.Instance.AddInimigoCount();
            }

            if (ProgressionManager.Instance != null)
            {
                ProgressionManager.Instance.AddXPTotal(ic.scoreDoInimigo); // XP permanente
            }

            ic.contabilizarPerda();
            ic.inimigoMorrendo();
            StartCoroutine(MorrerComDelay());
            return;
        }
        morrer(); //gambiarra pra separar inimigos de props
    }
    }

    protected virtual void AoReceberDano()
    {
        
    }
    
    


    IEnumerator MorrerComDelay()
    {
    yield return new WaitForSeconds(3f);

    if (this != null)
    {
        morrer();
    }
    }

    protected virtual void morrer()
{
    if (gameObject == null) return;

    if (ic != null)
    {
        ic.DroparGem();
    }

    Destroy(gameObject, 0.1f);
}
}