using UnityEngine;

public class wepAtaque : MonoBehaviour
{
    public float cooldown = 0.5f;
    private float tempoProximoTiro;

    public GameObject prefabTiro;

    public Transform pontoInicialDoTiro;


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
        Debug.Log("atirou");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direcao = mousePos - pontoInicialDoTiro.position;

        GameObject projetil = Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);
        projetil.GetComponent<Projetil>().definirDireção(direcao);
    }
}
