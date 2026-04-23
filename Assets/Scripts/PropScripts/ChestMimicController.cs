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

    


    void Start(){
        player = FindAnyObjectByType<PlayerController>();
        artfatoManager = FindAnyObjectByType<ArtfatoManager>();
        cardSelectionUI = FindAnyObjectByType<CardSelectionUI>();
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
            }
            else
            {
                invocarMimico();
            }
                
            }
    }

    private void definirMimico()
    {
        int definirMimico = UnityEngine.Random.Range(1,10);
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
        sr.enabled = false;
        collider2D.enabled = false;
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
