using UnityEngine;

public class WepShotgun : MonoBehaviour
{
    [Header("Configurações da Doze")]
    public int quantidadePelotas = 3;
    public float anguloAbertura = 30f;

    [Header("Rajada")]
    [SerializeField] private int tirosPorRajada = 2;
    [SerializeField] private float intervaloEntreTiros = 0.30f;
    [SerializeField] private float cooldownAposRajada = 1.2f;

    private int tirosRestantes;
    private float tempoProximoTiro;

    [Header("Referências")]
    public GameObject prefabTiro;
    public Transform pontoInicialDoTiro;
    public AudioClip fireSoundEffect;

    private PlayerController pc;
    private SoundController soundController;

    [SerializeField] private float danoBase;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        soundController = pc.soundController;

        tirosRestantes = tirosPorRajada;
    }

    private void Update()
    {
        Vector3 mouseatq = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseatq.z = 0f;

        Vector2 direcaoBase = (mouseatq - pontoInicialDoTiro.position).normalized;

        pc.anim.SetFloat("mousePosHorizontal", direcaoBase.x);
        pc.anim.SetFloat("mousePosVertical", direcaoBase.y);

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= tempoProximoTiro)
            {
                AtirarDoze(direcaoBase);

                tirosRestantes--;

                // Ainda tem tiros na rajada
                if (tirosRestantes > 0)
                {
                    tempoProximoTiro = Time.time + intervaloEntreTiros;
                }
                // Acabou a rajada
                else
                {
                    tempoProximoTiro = Time.time + cooldownAposRajada;
                    tirosRestantes = tirosPorRajada;
                }
            }
        }
    }

    void AtirarDoze(Vector2 direcaoPrincipal)
    {
        danoBase = pc.currentStatus.danoRanged / 2;

        float anguloCentro =
            Mathf.Atan2(direcaoPrincipal.y, direcaoPrincipal.x) * Mathf.Rad2Deg;

        for (int i = 0; i < quantidadePelotas; i++)
        {
            float variacao =
                (i - (quantidadePelotas - 1) / 2f) *
                (anguloAbertura / (quantidadePelotas - 1));

            float anguloFinal = anguloCentro + variacao;

            Vector2 direcaoTiro = new Vector2(
                Mathf.Cos(anguloFinal * Mathf.Deg2Rad),
                Mathf.Sin(anguloFinal * Mathf.Deg2Rad)
            );

            GameObject projetil =
                Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);

            Projetil proj = projetil.GetComponent<Projetil>();
            proj.Inicializar(direcaoTiro, danoBase);

            Destroy(projetil, 0.3f);
        }

        if (soundController != null)
            soundController.TocarSom(fireSoundEffect);
    }
}