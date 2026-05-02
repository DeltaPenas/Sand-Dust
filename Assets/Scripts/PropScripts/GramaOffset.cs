using UnityEngine;
using System.Collections.Generic;

public class GramaOffset : MonoBehaviour
{
    public float distanciaMax = 1.5f;
    public float forcaMax = 60f;
    public float suavidade = 5f;

    private float rotacaoAtual;
    private float velocidadeRotacao;

    private List<Transform> objetosDentro = new List<Transform>();

    void Update()
    {
        Transform alvo = PegarMaisProximo();

        float alvoRotacao = 0;

        if (alvo != null)
        {
            float distancia = Vector2.Distance(transform.position, alvo.position);

            if (distancia < distanciaMax)
            {
                Vector2 direcao = transform.position - alvo.position;
                float intensidade = 1 - (distancia / distanciaMax);

                alvoRotacao = direcao.x * forcaMax * intensidade;
            }
        }

        rotacaoAtual = Mathf.SmoothDamp(rotacaoAtual, alvoRotacao, ref velocidadeRotacao, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, rotacaoAtual);
    }

    Transform PegarMaisProximo()
    {
        Transform maisProximo = null;
        float menorDist = Mathf.Infinity;

        foreach (var obj in objetosDentro)
        {
            if (obj == null) continue;

            float dist = Vector2.Distance(transform.position, obj.position);

            if (dist < menorDist)
            {
                menorDist = dist;
                maisProximo = obj;
            }
        }

        return maisProximo;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        objetosDentro.Add(other.transform);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        objetosDentro.Remove(other.transform);
    }
}