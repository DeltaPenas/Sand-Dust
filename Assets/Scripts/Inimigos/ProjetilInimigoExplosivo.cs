using UnityEngine;

public class ProjetilInimgioExplosivo : MonoBehaviour // fiz mudanças para o projetil evitar depender do inimigo e 
// evitar algum bug
{
private Vector2 direcao;
[SerializeField]private GameObject explosionVFX;
[SerializeField]private AudioClip explosionSFX;

private float velocidade;
private int dano = 2;
private float tempoIgnorarColisao = 0.1f;
public float areaExpo = 1; 
private float tempoSpawn;

private void Start()
{
    tempoSpawn = Time.time;
    Invoke(nameof(Explodir), 3);

        
}

public void Inicializar(Vector2 direcaoInicial, float velocidadeProjetil, int danoProjetil)
{
        direcao = direcaoInicial.normalized;
        velocidade = velocidadeProjetil;
        dano = danoProjetil;
}

private void Update()
{
        transform.Translate(direcao * velocidade * Time.deltaTime);
}

private void OnTriggerEnter2D(Collider2D alvo)
{
        if (Time.time < tempoSpawn + tempoIgnorarColisao)
        return;
        
        //PlayerVida vida = alvo.GetComponent<PlayerVida>();

     
        if (!alvo.CompareTag("inimigo") && !alvo.CompareTag("Chão"))
        {
            Explodir();
        }
    }
private void Explodir()
    {
        SoundController soundController = FindAnyObjectByType<SoundController>();
        GameObject exp = Instantiate(
            explosionVFX,
            transform.position,
            Quaternion.identity
        );
        SpriteRenderer sr = exp.GetComponent<SpriteRenderer>();
        float tamanhoAtual = sr.bounds.size.x;

         float escala = (areaExpo * 0.2f)/tamanhoAtual;

        exp.transform.localScale = Vector3.one * escala;

        Collider2D[] alvos = Physics2D.OverlapCircleAll(transform.position, areaExpo);
        foreach (Collider2D alvo in alvos)
        {
            
            PlayerVida pv = alvo.GetComponent<PlayerVida>();

            if(pv != null)
            {
                pv.DarDanoPlayer(dano);
            }
        } 
        soundController.TocarSom(explosionSFX);
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.1f, 0.001f);
        Destroy(gameObject);
        




    }
}