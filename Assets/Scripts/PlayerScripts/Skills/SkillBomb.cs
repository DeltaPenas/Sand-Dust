using UnityEngine;

public class SkillBomb : SkillBase
{
    public GameObject bombPrefab;

    protected override void useSkill()
    {
        if (pontoSkill == null)
        {
            Debug.LogWarning("SkillBomb: pontoSkill é NULL");
            return;
        }

        if (bombPrefab == null)
        {
            Debug.LogError("SkillBomb: bombPrefab não definido!");
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direcao = (mousePos - pontoSkill.position).normalized;

        GameObject bomb = Instantiate(bombPrefab, pontoSkill.position, Quaternion.identity);

        BombProjetil proj = bomb.GetComponent<BombProjetil>();

        if (proj != null)
        {
            proj.definirDirecao(direcao);
            proj.raioExp = skillRange;
        }
        else
        {
            Debug.LogError("SkillBomb: BombProjetil não encontrado no prefab!");
        }
    }
}