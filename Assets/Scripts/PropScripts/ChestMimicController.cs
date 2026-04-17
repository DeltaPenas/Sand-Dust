using Unity.Mathematics;
using UnityEngine;

public class ChestMimicController : MonoBehaviour
{
    
    [SerializeField] private GameObject mimico;
    public int num = 20;
    public bool taDentro = false;
    public bool éMimico;
    private Vida vida;


    void Start(){
        definirMimico();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && taDentro)
            {
            if (!éMimico)
            {
                Destroy(gameObject);
            }
                invocarMimico();
            }
    }

    private void definirMimico()
    {
        int definirMimico = UnityEngine.Random.Range(1,10);
        Debug.Log(definirMimico);

        if(definirMimico >= 8)
        {
            éMimico = true;
        }
    }
    private void invocarMimico()
    {
        if (éMimico)
        {
            GameObject mimicoInimigo = Instantiate(
            mimico,
            transform.position,
            quaternion.identity
        );
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
