using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeponContainer : MonoBehaviour
{
    public GameObject cartaDeArma;
    public TextMeshProUGUI nomeWep;
    public TextMeshProUGUI descriçãoWep;
    public Image iconWep;
    public TextMeshProUGUI valorTextWep;

    private WeaponPickup pickupAtual; // ref do wep atual

    public void AtivarArma()
    {
        cartaDeArma.SetActive(true);
    }

    public void DesativarArma()
    {
        cartaDeArma.SetActive(false);
        pickupAtual = null; // limpa ref
    }

    public void SetPickupAtual(WeaponPickup pickup)
    {
        pickupAtual = pickup;
    }

    // botão da ui
    public void BotaoComprar()
    {
        if (pickupAtual != null)
        {
            pickupAtual.Comprar();
        }
    }
}