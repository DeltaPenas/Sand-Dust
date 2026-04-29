using System.Collections;
using Unity.Mathematics;
using Unity.Multiplayer;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Cartas/Efeitos/EfeitoDeExplosãoOnDash")]
public class EfeitoDeExplosãoOnDash : EfeitoCarta
{
    [SerializeField] private GameObject explosion;
    
    public override void Aplicar(PlayerController player)
    {
        player.OnDash += () =>
        {
            GameObject explo = Instantiate(
                explosion,
                player.transform.position,
                quaternion.identity
            );
        };
    }
}