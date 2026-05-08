using UnityEngine;

public class NPController : MonoBehaviour
{

    [SerializeField] private NPCData nPCData;
    
    [SerializeField] private GameObject caixaDeDialogo;
    [SerializeField] private GameObject interagirText;
    [SerializeField] private CaixaDeDialogoUI caixaDeDialogoUI;
    [SerializeField] private bool playerDentro;



    void Start()
    {
        nPCData = GetComponent<NPCData>();
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();


    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && playerDentro)
        {
            ChamarDialogoBox();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;
            AtivarInfoInteragir();


        
        
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;
            DesativarInfoInteragir();
            
        }
    }






    public void AtivarInfoInteragir()
    {
        interagirText.SetActive(true);

    }


    public void DesativarInfoInteragir()
    {
        interagirText.SetActive(false);

    }

    public void ChamarDialogoBox()
    {
        caixaDeDialogo.SetActive(true);
        Time.timeScale = 0f;
    }
    public void TirarDialogoBox()
    {
        caixaDeDialogo.SetActive(false);
        Time.timeScale = 1f;
    }









}