using System.Collections.Generic;
using UnityEngine;

public class ArtfatoManager : MonoBehaviour
{
    public ArtefatosDatabase database;
    public List<RaridadePeso> pesos;

    
    public List<Artefato> GerarOpções(int quantidade)
    {
        List<Artefato> copia = new List<Artefato>(database.todosOsArtefatos);
        List<Artefato> resultado = new List<Artefato>();

        for (int i =0; i < quantidade; i++)
        {
            Raridade r = SortearRaridade();

            Artefato escolhido = PegarArtefatoPorRaridade(r, copia);

            // fallback (caso não tenha daquela raridade)
            if (escolhido == null)
            {
            int index = Random.Range(0, copia.Count);
            escolhido = copia[index];
            }

            resultado.Add(escolhido);
            copia.Remove(escolhido);
        }
        return resultado;

    }

    Raridade SortearRaridade()
{
    int total = 0;

    foreach (var p in pesos)
        total += p.peso;

    int roll = Random.Range(0, total);

    int acumulado = 0;

    foreach (var p in pesos)
    {
        acumulado += p.peso;

        if (roll < acumulado) return p.raridade;
    }

    return Raridade.Comum;
}
    Artefato PegarArtefatoPorRaridade(Raridade r, List<Artefato> pool)
{
    var filtradas = pool.FindAll(c => c.raridade == r);

    if (filtradas.Count == 0)
        return null;

    return filtradas[Random.Range(0, filtradas.Count)];
}


    

}
