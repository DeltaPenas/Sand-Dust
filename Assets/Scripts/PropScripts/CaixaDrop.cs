using Unity.Mathematics;
using UnityEngine;

public class CaixaDrop : MonoBehaviour
{
    public GameObject hearth;



    void Start()
    {
        int PesoDrop = UnityEngine.Random.Range(0,9);

        if ( PesoDrop > 5)
        {
            GameObject coracao = Instantiate(hearth, transform.position, quaternion.identity);
        }   
    }

}