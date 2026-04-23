using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HeartUi heartUi;

    public float playerVidaTotal;
    public float playerVidaAtual;
    public bool playerIsEnvenenado;
    public bool playerIsQueimando;

    private bool coroutineIframeRodando = false;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        heartUi = FindAnyObjectByType<HeartUi>();
        definirVida();
        
    }

    public void definirVida()
    {
        playerVidaAtual = player.currentStatus.vidaMax;
        playerVidaTotal = playerVidaAtual;
        heartUi.UpdateHearts((int)playerVidaAtual, (int)playerVidaTotal);
        
    }

    public void DarDanoPlayer(float dano)
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

        heartUi.UpdateHearts((int)playerVidaAtual, (int)playerVidaTotal);
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.1f);

        if (player != null)
        {
            StartCoroutine(AtivarIframesTemporarios());
        }
    }

    public void CurarPlayer(float cura)
    {
        playerVidaAtual = Mathf.Min(playerVidaAtual + cura, playerVidaTotal);
        heartUi.UpdateHearts((int)playerVidaAtual, (int)playerVidaTotal);
    }

    public void morrer()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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