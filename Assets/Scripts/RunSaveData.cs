using System.Collections.Generic;

[System.Serializable]
public class RunSaveData
{
    public int xpColetado;
    public int inimigosMortos;
    public int salasConcluidas;
    public float tempoDeRun;

    public float playerVidaAtual;
    public float playerVidaTotal;

    public int moedasRun;

    public List<string> cartasColetadasIds = new List<string>();
    public List<string> efeitosAtivosIds = new List<string>();
}