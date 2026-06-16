using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

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
    [SerializeField] private GameObject version;
    [SerializeField] private GameObject telaDeMelhorias;
    [SerializeField] private GameObject telaDeOpções;
    [SerializeField] private GameObject telaDeCredito;
    [SerializeField] private GameObject painelDeProg;
     [SerializeField] private GameObject painelDeReset;

    [SerializeField] private CarroController carroController;
    [SerializeField] private FadeController fadeController;



    void Start()
    {
        carroController = FindAnyObjectByType<CarroController>();
        fadeController = FindAnyObjectByType<FadeController>();
    }

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
        danoSkillText.text = "Dano Skill: " + ProgressionManager.Instance.danoSkillBonus;
        danoUltText.text = "Dano Ultimate: " + ProgressionManager.Instance.danoUltBonus;
        velocidadeText.text = "Velocidade: " + ProgressionManager.Instance.velocidadeBonus;
    }
    public void ApagarProgressão()
    {
        ProgressionManager.Instance.ResetarProgresso();
    }
    public void ResetarProgressão()
    {
       ProgressionManager.Instance.RecuperarPontos();
    }

    public void IniciarRun()
    {
        StartCoroutine(SequenciaInicial());
        
    }

    public IEnumerator SequenciaInicial()
    {
        carroController.IniciarMove();
        telaInicial.SetActive(false);
        version.SetActive(false);
        yield return new WaitForSeconds (1.5f);
        fadeController.ChamarFade();
        //SoundController.instance.PlaySomFloresta();
        yield return new WaitForSeconds (1f);

        RunManager.Instance.IniciarCutscene();
      
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
    public void ChamarTelaDeReset()
    {
        painelDeReset.SetActive(true);
        telaDeMelhorias.SetActive(false);
    }
    public void TirarTelaDeReset()
    {
        painelDeReset.SetActive(false);
        telaDeMelhorias.SetActive(true);
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