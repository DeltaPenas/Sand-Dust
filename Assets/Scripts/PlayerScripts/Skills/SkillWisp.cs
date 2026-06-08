using Unity.Mathematics;
using UnityEngine;

public class SkillWisp : SkillBase
{
    [SerializeField] private GameObject wispObj;


    protected override bool TentaUsarSkill()
    {
        SpawnarWisp();
        return true;
    }


    private void SpawnarWisp()
    {
        if(wispObj == null) return;
       Vector3 offset = new Vector3(1.5f, 0, 0);

       GameObject wisp = Instantiate(wispObj, transform.position + offset, quaternion.identity);
       Wisp wisp1 = wisp.GetComponent<Wisp>();
       wisp1.DefinirAlvo(pc.transform);

    }
}