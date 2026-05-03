using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{
    public GameObject prefabArma;
    public int valor;
    public string nome;
    public string descr;
    public Sprite img;

    private WeponContainer wc;
    private PlayerController pc;
    private HeartUi heartUi;

    private PlayerController playerDentro; //  player dentro

    void Start()
    {
        wc = FindAnyObjectByType<WeponContainer>();
        pc = FindAnyObjectByType<PlayerController>();
        heartUi = FindAnyObjectByType<HeartUi>();
    }

    void Setup()
    {
        wc.valorTextWep.text = "" + valor;
        wc.nomeWep.text = nome;
        wc.descriçãoWep.text = descr;
        wc.iconWep.sprite = img;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = other.GetComponent<PlayerController>();

            wc.AtivarArma();
            Setup();

        
            wc.SetPickupAtual(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wc.DesativarArma();
            playerDentro = null;
        }
    }

    
    public void Comprar()
    {
        if (playerDentro == null) return;

        if (playerDentro.gems < valor) return;

        playerDentro.gems -= valor;
        heartUi.AtualizarGemas();

        WeaponManager wm = playerDentro.GetComponent<WeaponManager>();

        GameObject novaArma = Instantiate(prefabArma, playerDentro.transform);

        wm.AdicionarArma(novaArma);

        wc.DesativarArma();
        Destroy(gameObject);
    }
}