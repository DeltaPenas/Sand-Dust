using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeitos/EfeitoDeCura")]
public class EfeitoCura : EfeitoCarta
{

    
    public override void Aplicar(PlayerController player)
    {
        
        //player.pv.definirVida();
    }
}