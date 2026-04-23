using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Artefato/Novo Artefato")]
public class Artefato : ScriptableObject
{
   public string nome;
   public List<StatModifier> modifiers;

}
