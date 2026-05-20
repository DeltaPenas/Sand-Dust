using UnityEngine;

public class UltOrbital : UltBase
{
    [SerializeField]private GameObject projetilOrbitalPrefab;

    protected override bool TentaUsarUlt()
    {
      return true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnarOrbital();
        }
    }

    void SpawnarOrbital()
    {
        if(projetilOrbitalPrefab == null) return;
        Vector3 offset = new Vector3(1.5f, 0f, 0f);

        GameObject orbital = Instantiate(projetilOrbitalPrefab, transform.position + offset, Quaternion.identity);
        OrbitalProjetil orb = orbital.GetComponent<OrbitalProjetil>();

        if (orb != null)
        {
            orb.DefinirPlayer(transform);
        }



    }



}