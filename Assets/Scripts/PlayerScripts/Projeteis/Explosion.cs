using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float dano;
    public float areaExpo;
    private PlayerController pc;
    [SerializeField]private GameObject explosionVFX;
    [SerializeField]private AudioClip explosionSFX;



    void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        dano = pc.currentStatus.danoSkill;
        explodir();
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
            }
            soundController.TocarSom(explosionSFX);
            Camera.main.GetComponent<CameraShake>().ShakeCamera(0.5f, 0.01f);
            Destroy(gameObject);
    }
}