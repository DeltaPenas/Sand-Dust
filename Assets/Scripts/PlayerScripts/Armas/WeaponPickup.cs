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
    public string weaponID;

    private PlayerController playerDentro; //  player dentro

   void Start()
    {
        AtualizarReferencias();
    }

    void AtualizarReferencias()
    {
        if (wc == null)
            wc = FindAnyObjectByType<WeponContainer>();

        if (pc == null)
            pc = FindAnyObjectByType<PlayerController>();

        if (heartUi == null)
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
        if (playerDentro == null)return;

        WeaponManager wm = playerDentro.GetComponent<WeaponManager>();

        //tem a arma
        if (wm == null)
        {
            Debug.Log("WeaponManager Faltando");
            return;
        }
        

        if (playerDentro.gems < valor) return;

        GameObject novaArma = Instantiate(prefabArma, playerDentro.transform);

        bool comprou = wm.AdicionarArma(novaArma, weaponID);

        if (!comprou)
        {
            Destroy(novaArma); // evita duplicação
            return;
        }

        playerDentro.gems -= valor;
        RunManager.Instance.RemoveMoedasRun(valor);
        heartUi.AtualizarGemas();

        wc.DesativarArma();
        Destroy(gameObject);
    }
}