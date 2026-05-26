using UnityEngine;
using System.Collections.Generic;

public class CardSelectionUI : MonoBehaviour
{
 public GameObject painel;
 public List<CardUI> slots;
 private PlayerController player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        painel.SetActive(false);
    }
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.L))
    {   
        var manager = FindAnyObjectByType<ArtfatoManager>();
        MostrarArtefatos(manager.GerarOpções(3));
    }
    */
    }

    public void MostrarArtefatos(List<Artefato> artefatos)
    {
        painel.SetActive(true);
        Time.timeScale = 0f;
        for (int i = 0; i < slots.Count; i++)
    {
        if (i < artefatos.Count)
    {
        slots[i].gameObject.SetActive(true);
        slots[i].Setup(artefatos[i], this);
    }
        else
    {
        slots[i].gameObject.SetActive(false);
    }
    }
    }
    public void SelecionarArtefato(Artefato artefato)
    {
        foreach (var mod in artefato.modifiers)
        {
            player.AddModifier(mod);
        }
        foreach (var efeito in artefato.efeitos)
        {
            player.AddEfeito(efeito);
        }
        Fechar();
    }
    public void Fechar()
    {
        painel.SetActive(false);
        Time.timeScale = 1f;
    }

}
