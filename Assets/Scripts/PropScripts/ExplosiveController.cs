using Unity.Mathematics;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    private Vida vida;
    public int dano = 4;
    public float areaExpo;
    private Rigidbody2D rig;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private AudioClip explosionSFX;

    private void Start()
    {
        vida = GetComponent<Vida>();
        rig = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (vida.vidaAtual <= 0)
        {
            explodir();
        }
        
        
    }
    void explodir()
    {
            SoundController soundController = FindObjectOfType<SoundController>();
            GameObject exp = Instantiate(
                explosionVFX,
                transform.position,
                Quaternion.identity
            );

            SpriteRenderer sr = exp.GetComponent<SpriteRenderer>();
            float tamanhoAtual = sr.bounds.size.x;

            float escala = (areaExpo * 0.5f) / tamanhoAtual;

            exp.transform.localScale = Vector3.one * escala;

            Collider2D[] alvos = Physics2D.OverlapCircleAll(transform.position,areaExpo);
            foreach (Collider2D objAlvos in alvos)
            {
                Vida vidaInimigos = objAlvos.GetComponent<Vida>();
                PlayerVida vidaPlayer = objAlvos.GetComponent<PlayerVida>();

                if (vidaInimigos != null)
                {
                    vidaInimigos.receberDano(dano);
                }
                if (vidaPlayer != null)
                {
                    vidaPlayer.DarDanoPlayer(dano);
                }
            }
            soundController.TocarSom(explosionSFX);
            Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.1f);
            Destroy(gameObject);
    }
}
