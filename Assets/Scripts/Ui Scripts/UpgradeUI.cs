using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject telaInicial;
    [SerializeField] private GameObject telaDeMelhorias;
    [SerializeField] private GameObject telaDeOpções;
    [SerializeField] private GameObject telaDeCredito;
    [SerializeField] private GameObject painelDeProg;
    


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

    public void IniciarRun()
    {
        RunManager.Instance.StartRun();
        
    }

    public void ChamarTelaInicial()
    {
        telaInicial.SetActive(true);
        telaDeMelhorias.SetActive(false);
        telaDeOpções.SetActive(false);
        telaDeCredito.SetActive(false);
    }
    public void chamarTelaMelhorias()
    {
        telaInicial.SetActive(false);
        telaDeMelhorias.SetActive(true);
    }
    public void chamarTelaDeOpções()
    {
        telaInicial.SetActive(false);
        telaDeOpções.SetActive(true);
    }
    public void ChamarTelaDeCreditos()
    {
        telaInicial.SetActive(false);
        telaDeCredito.SetActive(true);
    }
    public void ChamarTelaDeProg()
    {
        painelDeProg.SetActive(true);
        telaDeOpções.SetActive(false);
    }
    public void TirarTelaDeProg()
    {
        painelDeProg.SetActive(false);
        telaDeOpções.SetActive(true);
    }




      public void Fechar()
    {
       Application.Quit();
        
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