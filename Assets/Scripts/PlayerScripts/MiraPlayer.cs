using System.Numerics;
using UnityEngine;

public class MiraPlayer : MonoBehaviour
{
    public UnityEngine.Vector3 mousePos;
    public UnityEngine.Vector2 direçãoMouse;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        direçãoMouse = mousePos - transform.position.normalized;


        
    }
}
