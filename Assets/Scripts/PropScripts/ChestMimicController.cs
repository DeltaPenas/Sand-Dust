using Unity.Mathematics;
using UnityEngine;

public class ChestMimicController : MonoBehaviour
{
    
    [SerializeField] private GameObject mimico;
    public int num = 20;
    public bool taDentro = false;




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && taDentro)
            {
                int definirMimico = UnityEngine.Random.Range(0, num);

                if(definirMimico >= 10)
                {
                    GameObject mimicoInimigo = Instantiate(
                        mimico,
                        transform.position,
                        quaternion.identity
                    );
                }
                Destroy(gameObject);
            }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            taDentro = true;
            Debug.Log("Jogador entrou na área!");
        }
    }

    // Chamado quando algo sai do Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            taDentro = false;
            Debug.Log("Jogador saiu da área!");
        }
    }




}
