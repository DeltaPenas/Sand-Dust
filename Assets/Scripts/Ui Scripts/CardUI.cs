using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI nomeTexto;
    public TextMeshProUGUI descriçãoTexto;
    
    
    public Image componenteIcone; 
    public Image componenteBg; 

    private Artefato artefato;
    private CardSelectionUI manager;
    private PlayerVida pv;

    void Start()
    {
        pv = FindAnyObjectByType<PlayerVida>();
    }
    
    public void Setup(Artefato novoArtefato, CardSelectionUI m)
    {
        artefato = novoArtefato;
        manager = m;
        
        nomeTexto.text = artefato.nome;
        descriçãoTexto.text = artefato.descrição;

        
        componenteIcone.sprite = artefato.icon;
        componenteBg.sprite = artefato.bg;
    }

    public void OnClick()
    {
        manager.SelecionarArtefato(artefato);
        pv.definirVida();
    }
}