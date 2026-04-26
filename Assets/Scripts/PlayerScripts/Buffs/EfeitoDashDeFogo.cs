using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeitos/EfeitoDashDeFogo")]
public class EfeitoDashDeFogo : EfeitoCarta
{
    
    public override void Aplicar(PlayerController player)
    {
        Debug.Log("Efeito aplicado!");

        player.OnDash += () =>
        {
            Debug.Log("Dash ativado!");
        };
    }
}