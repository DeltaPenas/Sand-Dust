using UnityEngine;

public class SkillBumerangue : SkillBase
{
    [Header("Referencias")]
    public GameObject boomObj;

    protected override bool TentaUsarSkill()
    {
        if (pontoSkill == null)
        {
            Debug.LogWarning("pontoSkill ta NULL");
            
        }
        if (boomObj == null)
        {
            Debug.LogError("prefab ta NULL");
       
        }

        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0f;

        Vector2 direção = (mousepos - pontoSkill.position).normalized;
        GameObject boom = Instantiate(boomObj, pontoSkill.position, Quaternion.identity);

        BumerangueProjetil boomProjetil = boom.GetComponent<BumerangueProjetil>();
        
        boomProjetil.Inicializar(pc.currentStatus.danoSkill, direção);

        return true;
    }
}