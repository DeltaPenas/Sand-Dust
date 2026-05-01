using UnityEngine;
using TMPro;
using System;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pontosText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI vidaText;
    [SerializeField] private TextMeshProUGUI danoRangedText;
    [SerializeField] private TextMeshProUGUI danoMeleeText;
    [SerializeField] private TextMeshProUGUI danoSkillText;
    [SerializeField] private TextMeshProUGUI danoUltText;
    [SerializeField] private TextMeshProUGUI velocidadeText;
    [SerializeField] private TextMeshProUGUI proximoLvlText;



    void Update()
    {
        pontosText.text = "Pontos: " + ProgressionManager.Instance.pontosDisponiveis;
        lvlText.text = "LVL: " + ProgressionManager.Instance.level;
        proximoLvlText.text = "Proximo lvl em: " + (ProgressionManager.Instance.xpParaProximoNivel - ProgressionManager.Instance.xpAtual);
        Setup();
    }

    void Setup()
    {
        vidaText.text = "Vida: "+ ProgressionManager.Instance.vidaBonus;
        danoRangedText.text = "Dano: " + ProgressionManager.Instance.danoRangedBonus;
        danoMeleeText.text = "Dano Melee: " + ProgressionManager.Instance.danoMeleeBonus;
        danoSkillText.text = "Dano Skill: " + ProgressionManager.Instance.danoSkillBonus;
        danoUltText.text = "Dano Ultimate: " + ProgressionManager.Instance.danoUltBonus;
        velocidadeText.text = "Velocidade de movimento: " + ProgressionManager.Instance.velocidadeBonus;
    }

    public void AddVida()
    {
        ProgressionManager.Instance.AddVida();
        Setup();
    }

    public void AddDano()
    {
        ProgressionManager.Instance.AddDanoRanged();
        Setup();
    }
    public void AddDanoMelee()
    {
        ProgressionManager.Instance.AddDanoMelee();
        Setup();
    }
    public void AddDanoSkill()
    {
        ProgressionManager.Instance.AddDanoSkill();
        Setup();
    }
    public void AddDanoUlt()
    {
        ProgressionManager.Instance.AddDanoUlt();
        Setup();
    }
    public void AddVelocidade()
    {
        ProgressionManager.Instance.AddVelocidade();
        Setup();
    }

}   