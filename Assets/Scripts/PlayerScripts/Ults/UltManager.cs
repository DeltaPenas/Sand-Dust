using UnityEngine;

public class UltManager : MonoBehaviour
{
    private UltBase ultAtual;
    public UltBase UltAtual => ultAtual;
    public System.Action<UltBase> OnUltChanged;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsarUlt();
        }
    }

    public void EquiparUlt(GameObject ultPrefab)
    {
        if (ultAtual != null)
        {
            Destroy(ultAtual.gameObject);
        }

        GameObject novaUlt = Instantiate(ultPrefab, transform);
        ultAtual = novaUlt.GetComponent<UltBase>();

        OnUltChanged?.Invoke(ultAtual);
    }

    public void UsarUlt()
    {
        if (ultAtual == null) return;
        ultAtual.tentaUsar();
    }


}