using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeitos/RicocheteInteligente")]
public class EfeitoRicochete : EfeitoCarta
{
    public float raioBusca = 5f;
    
    public GameObject prefabProjetil; 

    public override void Aplicar(PlayerController player)
    {
        player.OnRicochete += (proj) =>
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                proj.transform.position,
                raioBusca
            );

            foreach (var h in hits)
            {
                
                if (!h.CompareTag("inimigo")) continue;

                GameObject nova = Instantiate(
                    prefabProjetil,
                    proj.transform.position,
                    Quaternion.identity
                );

                var p = nova.GetComponent<Projetil>();

                Vector2 dir = (h.transform.position - proj.transform.position).normalized;

                

                
                p.podeGerarRicocheteExtra = false;

                Debug.Log("Ricochete inteligente ativado");

                break;
            }
        };
    }
}