using UnityEngine;
using TMPro;
public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI nomeTexto;
    private Artefato artefato;
    private CardSelectionUI manager;
        public void Setup(Artefato novoArtefato, CardSelectionUI m)
    {
        artefato = novoArtefato;
        manager = m;
        nomeTexto.text = artefato.nome;
    }

        public void OnClick()
    {
        manager.SelecionarArtefato(artefato);
    }


}
