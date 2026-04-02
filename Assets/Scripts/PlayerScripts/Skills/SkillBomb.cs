using UnityEngine;

public class SkillBomb : SkillBase
{
    public GameObject bombPrefab;
    public Transform pontoLançamento;
    public BombProjetil bmb;
    public SoundController sc;
    protected override void useSkill()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direcao = (mousePos - pontoLançamento.transform.position).normalized;

        GameObject bombProjetil = Instantiate(bombPrefab, pontoLançamento.position, Quaternion.identity);
        bombProjetil.GetComponent<BombProjetil>().definirDirecao(direcao);

        bmb.raioExp = skillRange;

        
        
    }

    

    
}