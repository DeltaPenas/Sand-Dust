using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PontoDeDecidaDeSala : MonoBehaviour
{
    private DungeonGeneratortest dg;
    private RunInfos runInfos;
    private TriggerDeTransicao tt;
    private PlayerVida pv;
    
    

    void Start()
    {
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        dg = FindAnyObjectByType<DungeonGeneratortest>();
        pv = FindAnyObjectByType<PlayerVida>();
        runInfos = FindAnyObjectByType<RunInfos>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dg != null && runInfos != null && runInfos.salasConcluidas >= dg.totalSalasCombate)
        {
            tt.FadeOut();
            Invoke(nameof(PodeDescer), 0.8f);
            dg.LimparInimigos();
            if(dg.isDevMode && pv.playerVidaAtual < pv.playerVidaTotal)
            {
                pv.CurarPlayer(1);
            }

            
            
        }
    }
    
    public void PodeDescer()
    {
            dg.LimparDungeon();
            runInfos.layer +=1;
            if(runInfos.layer > 3)
            {
                runInfos.layer = 0;
                runInfos.andar +=1;
                dg.qtdInimigos =1;
                dg.qtdmaxSalas +=1;
                dg.qtdminSalas +=1;
            }
        dg.qtdInimigos +=1;
        runInfos.salasConcluidas = 0;
    }
    

}
