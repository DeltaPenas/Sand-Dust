using UnityEngine;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

[CreateAssetMenu(menuName = "Artefato/Novo Artefato")]
public class Artefato : ScriptableObject
{
   public string nome;
   public string descrição;
   public Sprite icon;
   public Sprite bg;
   public Raridade raridade;
   public List<StatModifier> modifiers;
   public List<EfeitoCarta> efeitos;

}
