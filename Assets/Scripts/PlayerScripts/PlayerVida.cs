using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HeartUi heartUi;

    public int playerVidaTotal;
    public int playerVidaAtual;
    public bool playerIsEnvenenado;
    public bool playerIsQueimando;

    private bool coroutineIframeRodando = false;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        heartUi = FindAnyObjectByType<HeartUi>();

        playerVidaAtual = player.vida;
        playerVidaTotal = playerVidaAtual;

        heartUi.UpdateHearts(playerVidaAtual, playerVidaTotal);
    }

    public void DarDanoPlayer(int dano)
    {
        // Se o player estiver em iframe, não recebe dano
        if (player != null && player.iframeAtivo)
            return;

        // Garante que a vida nunca fique abaixo de 0
        playerVidaAtual = Mathf.Max(playerVidaAtual - dano, 0);

        if (playerVidaAtual <= 0)
        {
            morrer();
        }

        heartUi.UpdateHearts(playerVidaAtual, playerVidaTotal);
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.1f);

        if (player != null)
        {
            StartCoroutine(AtivarIframesTemporarios());
        }
    }

    public void CurarPlayer(int cura)
    {
        playerVidaAtual = Mathf.Min(playerVidaAtual + cura, playerVidaTotal);
        heartUi.UpdateHearts(playerVidaAtual, playerVidaTotal);
    }

    public void morrer()
    {
        Destroy(gameObject);
        // puxar o menu e resetar os bang
    }

    private System.Collections.IEnumerator AtivarIframesTemporarios()
    {
        // Se já houver uma coroutine de iframe rodando, não inicia outra
        if (coroutineIframeRodando)
            yield break;

        coroutineIframeRodando = true;

        player.iframeAtivo = true;

        // Espera o tempo de invulnerabilidade
        yield return new WaitForSeconds(player.iframetempo + player.iframeTempoBuff);

        player.iframeAtivo = false;
        coroutineIframeRodando = false;
    }
}