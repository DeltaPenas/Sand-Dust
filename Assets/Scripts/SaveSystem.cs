using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string path => Application.persistentDataPath + "/run.json";
    private static string progressionPath => Application.persistentDataPath + "/progression.json";

    public static void SaveProgression(ProgressionManager pm)
    {
        ProgressionSaveData data = new ProgressionSaveData();

        data.xpTotal = pm.xpTotal;
        data.xpAtual = pm.xpAtual;
        data.level = pm.level;
        data.pontosDisponiveis = pm.pontosDisponiveis;

        data.vidaBonus = pm.vidaBonus;
        data.danoRangedBonus = pm.danoRangedBonus;
        data.danoMeleeBonus = pm.danoMeleeBonus;
        data.danoSkillBonus = pm.danoSkillBonus;
        data.danoUltBonus = pm.danoUltBonus;
        data.velocidadeBonus = pm.velocidadeBonus;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(progressionPath, json);

        Debug.Log("Progressão salva!");
    }

    public static void SaveRun(RunData run, PlayerVida playerVida)
    {
        RunSaveData data = new RunSaveData();

        data.moedasRun = run.moedasRun;
        data.cartasColetadasIds = new List<string>(run.cartasColetadasIds);
        data.efeitosAtivosIds = new List<string>(run.efeitosAtivosIds);
        data.xpColetado = run.xpColetado;
        data.inimigosMortos = run.inimigosMortos;
        data.salasConcluidas = run.salasConcluidas;
        data.tempoDeRun = run.tempoDeRun;

        data.playerVidaAtual = playerVida.playerVidaAtual;
        data.playerVidaTotal = playerVida.playerVidaTotal;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("SALVANDO EM: " + path);
    }

    public static void LoadProgression(ProgressionManager pm)
    {
        if (!File.Exists(progressionPath))
            return;

        string json = File.ReadAllText(progressionPath);
        ProgressionSaveData data = JsonUtility.FromJson<ProgressionSaveData>(json);

        pm.xpTotal = data.xpTotal;
        pm.xpAtual = data.xpAtual;
        pm.level = data.level;
        pm.pontosDisponiveis = data.pontosDisponiveis;

        pm.vidaBonus = data.vidaBonus;
        pm.danoRangedBonus = data.danoRangedBonus;
        pm.danoMeleeBonus = data.danoMeleeBonus;
        pm.danoSkillBonus = data.danoSkillBonus;
        pm.danoUltBonus = data.danoUltBonus;
        pm.velocidadeBonus = data.velocidadeBonus;

        Debug.Log("Progressão carregada!");
    }

    public static void LoadRun(RunData run, PlayerVida playerVida)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Nenhum save encontrado.");
            return;
        }

        string json = File.ReadAllText(path);
        RunSaveData data = JsonUtility.FromJson<RunSaveData>(json);

        run.xpColetado = data.xpColetado;
        run.inimigosMortos = data.inimigosMortos;
        run.salasConcluidas = data.salasConcluidas;
        run.tempoDeRun = data.tempoDeRun;
        run.moedasRun = data.moedasRun;
        run.cartasColetadasIds = new List<string>(data.cartasColetadasIds);
        run.efeitosAtivosIds = new List<string>(data.efeitosAtivosIds);

        playerVida.playerVidaAtual = data.playerVidaAtual;
        playerVida.playerVidaTotal = data.playerVidaTotal;
        playerVida.MarcarVidaCarregada();

        Debug.Log("Run carregada.");
    }
    public static void DeleteRunSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save da run apagado.");
        }
    }
}