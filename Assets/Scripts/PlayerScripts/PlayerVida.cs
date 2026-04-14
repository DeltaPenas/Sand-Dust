using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerVida : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HeartUi heartUi;
    public int playerVidaTotal;
    public int playerVidaAtual;
    public bool playerIsEnvenenado;
    public bool playerIsQueimando;
    
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
        playerVidaAtual -=dano;

        if (playerVidaAtual <= 0)
        {
            morrer();
        }
        heartUi.UpdateHearts(playerVidaAtual, playerVidaTotal);
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.1f);
    }
    public void CurarPlayer(int cura)
    {
        playerVidaAtual +=cura;
        heartUi.UpdateHearts(playerVidaAtual, playerVidaTotal);
    }
   public void morrer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

}
