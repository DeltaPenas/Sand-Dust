using System.Collections;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeitos/EfeitoDashDeFogo")]
public class EfeitoDashDeFogo : EfeitoCarta
{
    public GameObject fogoObj;


  
    public override void Aplicar(PlayerController player)
    {
        Debug.Log("Efeito aplicado!");
        

        player.OnDash += () =>
        {
            player.StartCoroutine(SpawnarFogo(player));
        };

        

        
        
        
    }
        
     
        public IEnumerator SpawnarFogo(PlayerController player)
        {   
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds (0.02f);
            GameObject fogo = Instantiate(
                fogoObj,
                player.transform.position,
                quaternion.identity
            ); 
        }
        
        }  
}