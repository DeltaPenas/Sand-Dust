using UnityEngine;

public class InteractSystem : MonoBehaviour
{

    
    public NPController nPController;
    private CaixaDeDialogoUI caixaDeDialogoUI;

    void Start()
    {
        nPController = GetComponentInParent<NPController>();
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            nPController.playerDentro = true;

            caixaDeDialogoUI.interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            nPController.playerDentro = false;

            caixaDeDialogoUI.interactText.SetActive(false);
        }
    }
}