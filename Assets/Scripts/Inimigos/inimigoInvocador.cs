using UnityEngine; 
public class InimigoInvocador : InimigoBase_Ranged
{ 
    [Header("Invocação")]
    public GameObject inimigoPrefab;
    public Transform pontoInvocacao; 
    public int quantidadeInvocada = 1;
    public int maxInimigos = 5;
    private int inimigosAtuais = 0;
    [Header("Cooldown De Invocação")]
    public float intervaloInvocacao = 3f;
    private float proximaInvocacao;
    void Invocar()
    {
        if (inimigosAtuais >= maxInimigos) return;

        for (int i = 0; i < quantidadeInvocada; i++)
        {
            Vector2 posicaoSpawn;

            if (pontoInvocacao != null)
            {
                posicaoSpawn = pontoInvocacao.position;
            }
            else
            {
                posicaoSpawn = (Vector2)transform.position + Random.insideUnitCircle * 1.5f;
            }

            GameObject novo = Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);

            // pega o script do inimigo que foi invocado
            Inimigo inv = novo.GetComponent<Inimigo>();

            // se tiver o script, conecta com o invocador
            if (inv != null)
            {
                inv.invocador = this;
            }

            inimigosAtuais++;
        } 
    }
    protected override void Comportamento()
    {
                float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= 4f && distancia > distanciaParada && Time.time >= proximaInvocacao)
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