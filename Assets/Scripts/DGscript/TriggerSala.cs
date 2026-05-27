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

        if (!other.CompareTag("Player"))
            return;

        if (MinimapManager.Instance != null)
            MinimapManager.Instance.RevelarSala(sala);
        
        MinimapManager.Instance.RevelarSala(sala);

        if (sala == null)
        {
            Debug.LogError("Sala não encontrada");
            return;
        }

        Debug.Log("Tipo da sala: " + sala.tipoSala);

        if (sala.tipoSala == TipoSala.SalaBoss)
        {
            Debug.Log("Iniciando boss fight");
            sala.IniciarBossFight();
            return;
        }

        if (sala.entrou || sala.salaLimpa)
            return;

        sala.AtivarSala();
    }
}