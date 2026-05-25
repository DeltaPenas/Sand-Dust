using System.Collections;
using UnityEngine;

public class PontoDeDecidaDeSala : MonoBehaviour
{
    private DungeonGeneratortest dg;
    private TriggerDeTransicao tt;
    private PlayerVida pv;
    private CaixaDeDialogoUI caixaDeDialogoUI;

    public GameObject portalAtivo;
    public GameObject portalDesligado;
    public GameObject portalsIluminaçao;

    private bool playerDentro;
    private bool descendo;
    private float cooldownInteracao;

    void Start()
    {
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        dg = FindAnyObjectByType<DungeonGeneratortest>();
        pv = FindAnyObjectByType<PlayerVida>();
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();
    }

    private void Update()
    {
        cooldownInteracao -= Time.deltaTime;

        if (cooldownInteracao > 0f) return;

        if (descendo) return;

        if (!playerDentro) return;

        if (dg == null) return;

        if (RunManager.Instance.currentRun.salasConcluidas < dg.totalSalasCombate)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SequenciaDescida());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (dg == null) return;

        if (RunManager.Instance.currentRun.salasConcluidas < dg.totalSalasCombate)
            return;

        playerDentro = true;

        if (caixaDeDialogoUI != null)
            caixaDeDialogoUI.interactText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerDentro = false;

        if (caixaDeDialogoUI != null)
            caixaDeDialogoUI.interactText.SetActive(false);
    }

    IEnumerator SequenciaDescida()
    {
        descendo = true;

        if (caixaDeDialogoUI != null)
            caixaDeDialogoUI.interactText.SetActive(false);

        tt.FadeOut();

        yield return new WaitForSeconds(0.8f);

        dg.LimparInimigos();

        PodeDescer();

        cooldownInteracao = 0.5f;

        descendo = false;
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