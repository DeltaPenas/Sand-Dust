using UnityEngine; 
public class InimigoInvocador : InimigoBase // tem que usar o prefab da unity para a invocação funcionar certinho
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
public float distanciaInvocacao = 4f;

protected override Vector2 DirecaoBase()
    {
        if (distanciaPlayer < distanciaInvocacao)
            return (transform.position - player.position).normalized;

        return Vector2.zero;
    }
void Invocar()
    {
        if (inimigosAtuais >= maxInimigos) return;

        int quantidadeReal = Mathf.Min(quantidadeInvocada, maxInimigos - inimigosAtuais);

        for (int i = 0; i < quantidadeReal; i++)
        {
            Vector2 posicaoSpawn;

            if (pontoInvocacao != null)
                posicaoSpawn = pontoInvocacao.position;
            else
                posicaoSpawn = (Vector2)transform.position + Random.insideUnitCircle.normalized * 2f;

            GameObject novo = Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);

            InimigoBase inimigo = novo.GetComponent<InimigoBase>();

            if (inimigo != null)
            {
                inimigo.invocador = this;
            }

            inimigosAtuais++;
        }
    }
protected override void Comportamento()
    {
    if (distanciaPlayer <= distanciaInvocacao &&
        Time.time >= proximaInvocacao)
        {
            Invocar();
            proximaInvocacao = Time.time + intervaloInvocacao;
        }
    }
public void InimigoMorreu()
    {
            inimigosAtuais = Mathf.Max(0, inimigosAtuais - 1); //para evitar puchar numeros negativos nas invocações
    }
}