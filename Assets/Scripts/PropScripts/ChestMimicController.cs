using NUnit.Framework.Internal;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChestMimicController : MonoBehaviour
{
    
    [SerializeField] private GameObject mimico;
    public int num = 20;
    public bool taDentro = false;
    public bool jaAbriu;
    public bool éMimico;
    private Vida vida;
    private SpriteRenderer sr;
    private BoxCollider2D collider2D;
    private PlayerController player;
    private ArtfatoManager artfatoManager;
    private CardSelectionUI cardSelectionUI;
    private CaixaDeDialogoUI caixaDeDialogoUI;
    

    


    void Start(){
        player = FindAnyObjectByType<PlayerController>();
        artfatoManager = FindAnyObjectByType<ArtfatoManager>();
        cardSelectionUI = FindAnyObjectByType<CardSelectionUI>();
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();
        collider2D = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        definirMimico();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && taDentro)
            {
            if (!éMimico && !jaAbriu)
            {
                
                abrirBau();
                jaAbriu = true;
            }
            else
            {
                invocarMimico();
            }
                
            }
    }

    private void definirMimico()
    {
        int definirMimico = UnityEngine.Random.Range(0,10);
        Debug.Log(definirMimico);

        if(definirMimico >= 9)
        {
            éMimico = true;
        }
    }

    private void abrirBau()
    {
        var opcoes = artfatoManager.GerarOpções(3);
        cardSelectionUI.MostrarArtefatos(opcoes);
        jaAbriu = true;
        caixaDeDialogoUI.interactText.SetActive(false);
    }
    private void invocarMimico()
    {
        if (éMimico !=jaAbriu)
        {
            GameObject mimicoInimigo = Instantiate(
            mimico,
            transform.position,
            quaternion.identity
        );
        Destroy(gameObject);
        sr.enabled = false;
        collider2D.enabled = false;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            taDentro = true;
            caixaDeDialogoUI.interactText.SetActive(true);
            Debug.Log("Jogador entrou na área!");
        }
    }

    // Chamado quando algo sai do Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            taDentro = false;
            caixaDeDialogoUI.interactText.SetActive(false);

            Debug.Log("Jogador saiu da área!");
        }
    }




}
