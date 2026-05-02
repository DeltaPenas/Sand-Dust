using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject prefabArma;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponManager wm = other.GetComponent<WeaponManager>();

            GameObject novaArma = Instantiate(prefabArma, other.transform);

            wm.AdicionarArma(novaArma);

            Destroy(gameObject);
        }
    }
}