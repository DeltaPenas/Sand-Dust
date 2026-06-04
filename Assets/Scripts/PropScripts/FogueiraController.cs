using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FogueiraController : MonoBehaviour
{
    private SpriteRenderer sp;
    private ArtfatoManager artfatoManager;
    private CardSelectionUI cardSelectionUI;
    private CaixaDeDialogoUI caixaDeDialogoUI;
    public SoundController sc;
    
    private PlayerVida pv;
    private bool taDentro;

    [SerializeField] private GameObject fogueiraDesligada;
    [SerializeField] private AudioClip ac;
    
    


    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        artfatoManager = FindAnyObjectByType<ArtfatoManager>();
        cardSelectionUI = FindAnyObjectByType<CardSelectionUI>();
        caixaDeDialogoUI = FindAnyObjectByType<CaixaDeDialogoUI>();
        pv = FindAnyObjectByType<PlayerVida>();
        sc = FindAnyObjectByType<SoundController>();
        
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && taDentro)
        {
            UsarFogueira();
        }
    }

    void UsarFogueira()
    {
        GameObject fogueiraDesligadaObj = Instantiate(fogueiraDesligada, transform.position, quaternion.identity);

        if (pv.playerVidaAtual < pv.playerVidaTotal)
        {
           pv.RestaurarVida(); 
        }

        sc.TocarSom(ac);
        var opcoes = artfatoManager.GerarOpções(3);
        cardSelectionUI.MostrarArtefatos(opcoes);
        caixaDeDialogoUI.interactText.SetActive(false);

        Destroy(gameObject);

        
      
        
        
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