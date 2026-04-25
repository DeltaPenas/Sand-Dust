using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeitos/RicocheteInteligente")]
public class EfeitoRicochete : EfeitoCarta
{
    public float raioBusca = 5f;
    

    public override void Aplicar(PlayerController player)
    {
        
        player.OnRicochete += (proj) =>
        
        
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(proj.transform.position, raioBusca);
            
            

            foreach (var h in hits)
            {
                if (h.CompareTag("inimigo") || h.CompareTag("Parede"))
                {
                    GameObject nova = GameObject.Instantiate(proj, proj.transform.position, Quaternion.identity);

                    Vector2 dir = (h.transform.position - proj.transform.position).normalized;

                    var p = nova.GetComponent<Projetil>();
                    p.definirDireção(dir);

                    Debug.Log("Ricochete ativo");
                    break;
                }
            }
        };
    }
}