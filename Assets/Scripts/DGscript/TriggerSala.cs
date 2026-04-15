using UnityEngine;

public class TriggerSala : MonoBehaviour
{
    private SalaController sala;

    private void Awake()
    {
        sala = GetComponentInParent<SalaController>();
        Debug.Log("Sala encontrada: " + sala);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entrou no trigger: " + transform.parent.name);
        Debug.Log("Quantidade de inimigos: " + sala.qtdInimigosVivos);

        if (!other.CompareTag("Player"))
            return;

        if (sala == null)
        {
            Debug.LogError("Sala não encontrada");
            return;
        }

        if (sala.entrou || sala.salaLimpa)
            return;

        sala.AtivarSala();

         if (sala.salaLimpa)
        {
        sala.LiberarPortas();
        }

    }
}