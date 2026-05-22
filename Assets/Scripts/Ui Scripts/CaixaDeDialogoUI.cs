using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaixaDeDialogoUI : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private GameObject caixaDeDialogo;
    [Header("Textos")]
    [SerializeField] private TMP_Text nomeNPCText;
    [SerializeField] private TMP_Text falaNPCText;
    [SerializeField] private TMP_Text falaPlayerText;

    [Header("Botões")]
    [SerializeField] private Button continuarButton;
    [SerializeField] private Button lojaButton;
    [SerializeField] private Button sairButton;
    

    private NPCData npcAtual;
    private NPController npcControllerAtual;
    public GameObject interactText;
    
    private int indexDialogo;


    public void IniciarDialogo(NPCData npc, NPController controller)
    {
        npcAtual = npc;
        npcControllerAtual = controller;

        indexDialogo = 0;
        interactText.SetActive(false);
        caixaDeDialogo.SetActive(true);

        AtualizarDialogo();
    }

    void AtualizarDialogo()
    {
        nomeNPCText.text = npcAtual.nome;

        falaNPCText.text = npcAtual.falasNPC[indexDialogo];
        falaPlayerText.text = npcAtual.falasPlayer[indexDialogo];

        bool ultimaFala = indexDialogo >= npcAtual.falasNPC.Count - 1;

        continuarButton.gameObject.SetActive(!ultimaFala);
     
        
        if (npcAtual.tipoNPC == NPCtype.shop)
        {
            lojaButton.gameObject.SetActive(true);
        }
        else
        {
            lojaButton.gameObject.SetActive(false);
        }
        
       
    }

    public void ProximaFala()
    {
        indexDialogo++;

        if(indexDialogo >= npcAtual.falasNPC.Count)
        {
            FecharDialogoUI();
            return;
        }

        AtualizarDialogo();
    }

    public void AbrirLoja()
    {
        npcControllerAtual.AbrirLoja();
        interactText.SetActive(false);
    }

    public void FecharDialogoUI()
    {
        Time.timeScale =1;
        interactText.SetActive(true);
        caixaDeDialogo.SetActive(false);
    }
}