[System.Serializable]
public class RunData
{
    public int inimigosMortos;
    public int salasConcluidas;
    public int PlayerScore;
    public float tempoDeRun;
    public int xpColetado;

    public void reset()
    {
        inimigosMortos = 0;
        salasConcluidas = 0;
        PlayerScore = 0;
        tempoDeRun = 0;
        xpColetado = 0;
    }

}

