using UnityEngine;

public abstract class UltBase : MonoBehaviour
{
    public float ultRange;
    public int ultDmg;
    public float ultCooldown;
    public float ultimoUsoUlt;

    private void Start()
    {
        ultimoUsoUlt = -ultCooldown;
    }

    public virtual bool podeUsarUlt()
    {
        return Time.time >= ultimoUsoUlt + ultCooldown;
    }

    public void tentaUsar()
    {
        if (podeUsarUlt())
        {
            
            bool conseguiuUsar = tentaUsarUlt();

            
            if (conseguiuUsar)
            {
                ultimoUsoUlt = Time.time;
            }
        }
    }

    
    protected abstract bool tentaUsarUlt();
}