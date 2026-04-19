using System.Runtime.CompilerServices;
using UnityEngine; 
public class InimigoInvocador : InimigoRanged
{ 
    private Vida vida;

    [Header("Invocação")]
    public CatalogoInimigos catalogoInimigos;
    public Transform pontoInvocacao; 
    public int quantidadeInvocada = 1;
    public int maxInimigos = 5;
    private int inimigosAtuais = 0;
    [Header("Cooldown De Invocação")]
    public float intervaloInvocacao = 3f;
    private float proximaInvocacao;

    private void Start()
    {
        vida = GetComponent<Vida>();
    }

    void Invocar()
    {
        if (inimigosAtuais >= maxInimigos && vida.vidaAtual > 0) return;

        for (int i = 0; i < quantidadeInvocada; i++)
        {
            Vector2 posicaoSpawn;

            if (pontoInvocacao != null)
            {
                posicaoSpawn = pontoInvocacao.position;
            }
            else
            {
                posicaoSpawn = (Vector2)transform.position + Random.insideUnitCircle * (1.5f + 2f);
            }
            

            int indice = Random.Range(0, catalogoInimigos.inimigosInvocaveisPeloInvocadordeInimigos.Count);

            GameObject inimigoPrefab = catalogoInimigos.inimigosInvocaveisPeloInvocadordeInimigos[indice];

            GameObject novo = Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);

            // pega o script do inimigo que foi invocado
            InimigoRanged inv = novo.GetComponent<InimigoRanged>();


            inimigosAtuais++;
        } 
    }
    protected override void Comportamento()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= 4f && Time.time >= proximaInvocacao)
        {
            Invocar();
            proximaInvocacao = Time.time + intervaloInvocacao;
        }
    }
    public void InimigoMorreu()
    {
        inimigosAtuais--; //ainda preciso adicionar um rastreador na invocação para ver a certinha qual inimigo morreu
    }
}