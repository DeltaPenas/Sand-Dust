using System.Collections.Generic;
[System.Serializable]
public class RunData
{
    public int inimigosMortos;
    public int salasConcluidas;
    public int PlayerScore;
    public float tempoDeRun;
    public int xpColetado;
    public int moedasRun;
    public List<string> cartasColetadasIds = new List<string>();
    public List<string> efeitosAtivosIds = new List<string>();

    public void reset()
    {
        inimigosMortos = 0;
        salasConcluidas = 0;
        PlayerScore = 0;
        tempoDeRun = 0;
        xpColetado = 0;
        moedasRun = 0;

        cartasColetadasIds.Clear();
        efeitosAtivosIds.Clear();
    }

}

