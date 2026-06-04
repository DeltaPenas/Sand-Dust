using UnityEngine;

public class CarroController : MonoBehaviour
{
    float velocidade = 7;
    bool taMovendo;
    
 

    
    void Update()
    {
        if (taMovendo)
        {
            MoverDireita(); 
        }
       
    }
    public void IniciarMove()
    {
        taMovendo = true;
    }


    void MoverDireita()
    {
        
        
         transform.position += (Vector3)Vector2.right * velocidade * Time.deltaTime;
        }
        
    }


