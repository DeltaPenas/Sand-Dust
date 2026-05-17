using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HeartUi heartUi;
    [SerializeField] private DanoVisual dv;
    [SerializeField] private AudioClip danoPlayerSound;
    [SerializeField] private SoundController soundController;

    public float playerVidaTotal;
    public float playerVidaAtual;
    public bool playerIsEnvenenado;
    public bool playerIsQueimando;

    private bool coroutineIframeRodando = false;

    private bool vidaJaCarregada = false;


    public void MarcarVidaCarregada()
    {
        vidaJaCarregada = true;
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        heartUi = FindAnyObjectByType<HeartUi>();
        definirVida();
        player = FindAnyObjectByType<PlayerController>();

        player.OnVidaMaxChanged += AtualizarVida;
        soundController = FindAnyObjectByType<SoundController>();
        
    }

    public void definirVida()
{
    playerVidaTotal = player.currentStatus.vidaMax;

    // Só define vida cheia se ainda não existir save/carregamento
    if (!vidaJaCarregada)
    {
        playerVidaAtual = playerVidaTotal;
    }

    heartUi.UpdateHearts((int)playerVidaAtual, (int)playerVidaTotal);
}
    void AtualizarVida(float novaVidaMax)
    {
        float vidaMaxAntiga = playerVidaTotal;
        playerVidaTotal = novaVidaMax;

        float diferenca  = novaVidaMax - vidaMaxAntiga;

        //diferença entre as vidas

        if  (diferenca > 0)
        {
            playerVidaAtual +=diferenca;    
        }

        playerVidaAtual = Mathf.Clamp(playerVidaAtual, 0, playerVidaTotal);
        heartUi.UpdateHearts((int)playerVidaAtual, (int)playerVidaTotal);

        Debug.Log("Vida atualizada!");



    }

    public void DarDanoPlayer(float dano)
    {
        // Se o player estiver em iframe, não recebe dano
        if (player != null && player.iframeAtivo)
            return;

        // Garante que a vida nunca fique abaixo de 0
        playerVidaAtual = Mathf.Max(playerVidaAtual - dano, 0);
        soundController.TocarSom(danoPlayerSound);

        if (dv != null)
        {
            dv.TomouDano();
        }

        if (playerVidaAtual <= 0)
        {
            morrer();
        }

        heartUi.UpdateHearts((int)playerVidaAtual, (int)playerVidaTotal);

        if (RunManager.Instance != null)
            RunManager.Instance.SaveCurrentRun();
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

        if (RunManager.Instance != null)
            RunManager.Instance.SaveCurrentRun();
    }

    public void morrer()
    {
        RunManager.Instance.EndRun();
        
        
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

