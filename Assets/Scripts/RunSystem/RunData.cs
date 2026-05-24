using System.Collections.Generic;
[System.Serializable]
public class RunData
{
    public int inimigosMortos;
    public float inimigoLifeBuff;
    public float inimigoBuffDano;
    public int salasConcluidas;
    public int PlayerScore;
    public float tempoDeRun;
    public int xpColetado;
    public int moedasRun;
    public int layer;
    public int andar;
    public List<string> cartasColetadasIds = new List<string>();
    public List<string> efeitosAtivosIds = new List<string>();

    public void reset()
    {
        inimigosMortos = 0;
        inimigoLifeBuff = 0;
        inimigoBuffDano = 0;
        salasConcluidas = 0;
        PlayerScore = 0;
        tempoDeRun = 0;
        xpColetado = 0;
        moedasRun = 0;
        layer = 0;
        andar = 0;

        cartasColetadasIds.Clear();
        efeitosAtivosIds.Clear();
    }

}

