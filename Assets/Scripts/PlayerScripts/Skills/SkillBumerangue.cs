using UnityEngine;


public class SkillBumerangue : SkillBase
{   
    [Header("Referencias")]
    public GameObject boomObj;
    private Rigidbody2D rig;

    [Header("Status")]

    public int dano;

    public void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    protected override bool TentaUsarSkill()
    {

        return true;
    }
}