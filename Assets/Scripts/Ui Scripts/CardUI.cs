using UnityEngine;
using TMPro;
public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI nomeTexto;
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
    }

        public void OnClick()
    {
        manager.SelecionarArtefato(artefato);
        pv.definirVida();
    }


}
