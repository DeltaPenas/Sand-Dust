using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PontoDeDecidaDeSala : MonoBehaviour
{
    private DungeonGeneratortest dg;
    private TriggerDeTransicao tt;
    private PlayerVida pv;
    public GameObject portalAtivo;
    public GameObject portalDesligado;
    public GameObject portalsIluminaçao;




    void Start()
    {
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        dg = FindAnyObjectByType<DungeonGeneratortest>();
        pv = FindAnyObjectByType<PlayerVida>();
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dg != null && RunManager.Instance.currentRun.salasConcluidas >= dg.totalSalasCombate)
        {

            tt.FadeOut();
            Invoke(nameof(PodeDescer), 0.8f);
            dg.LimparInimigos();
            
        }
    }
    public void AtivarPortal()
    {
        portalAtivo.SetActive(true);
        portalDesligado.SetActive(false);
        portalsIluminaçao.SetActive(true);
    }
  
    
    public void PodeDescer()
{
    RunManager.Instance.currentRun.layer++;
    RunManager.Instance.currentRun.inimigoLifeBuff += 0.25f;

    // CHEGOU NO BOSS
    if (RunManager.Instance.currentRun.layer == 5)
    {
        Debug.Log("INDO PRA MINI DUNGEON DO BOSS");

        dg.LimparDungeon();

        RunManager.Instance.currentRun.salasConcluidas = 0;

        return;
    }

    // PASSOU DO BOSS -> NOVO ANDAR
    if (RunManager.Instance.currentRun.layer > 5)
    {
        Debug.Log("NOVO ANDAR");

        RunManager.Instance.currentRun.layer = 1;
        RunManager.Instance.currentRun.andar++;
        RunManager.Instance.currentRun.inimigoBuffDano++;

        dg.qtdInimigos = 1;
        dg.qtdmaxSalas += 1;
        dg.qtdminSalas += 1;
    }
    else
    {
        dg.qtdInimigos += 1;
    }

    dg.LimparDungeon();

    RunManager.Instance.currentRun.salasConcluidas = 0;
}
    

}
