using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    [Header("Info")]

    public NPCtype tipoNPC;
    public string nome;
    [TextArea]
    public List<string> falasNPC;

    [TextArea]
    public List<string> falasPlayer;


}
