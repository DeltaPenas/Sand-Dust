using UnityEngine;

public class wepAtaque : MonoBehaviour
{
    public float cooldown = 0.5f;
    private float tempoProximoTiro;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TentarAtacar();
        }
    }

    void TentarAtacar()
    {
        if (Time.time >= tempoProximoTiro)
        {
            Atacar();
            tempoProximoTiro = Time.time + cooldown;
        }
    }

    void Atacar()
    {
        Debug.Log("Ataque executado");
    }
}
