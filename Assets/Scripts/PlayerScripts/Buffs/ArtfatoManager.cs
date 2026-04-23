using System.Collections.Generic;
using UnityEngine;

public class ArtfatoManager : MonoBehaviour
{
    public ArtefatosDatabase database;

    
    public List<Artefato> GerarOpções(int quantidade)
    {
        List<Artefato> copia = new List<Artefato>(database.todosOsArtefatos);
        List<Artefato> resultado = new List<Artefato>();

        for (int i =0; i < quantidade; i++)
        {
            if(copia.Count == 0) break;

            int index = Random.RandomRange(0, copia.Count);
            resultado.Add(copia[index]);
            copia.RemoveAt(index);
        }
        return resultado;

    }
    

}
