using UnityEngine;

public class OrbitalProjetil : MonoBehaviour
{
    public float velocidadeRotacao = 180f;
    public float distancia = 1.5f;
    public float rotacaoInterna = 500f;

    private float angulo;

    public Transform player;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (player == null) return;

        angulo += velocidadeRotacao * Time.deltaTime;

        float rad = angulo * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0
        ) * distancia;

        transform.position = player.position + offset;

        transform.Rotate(0, 0, rotacaoInterna * Time.deltaTime);
    }

    public void DefinirPlayer(Transform alvo)
    {
        player = alvo;
    }
}