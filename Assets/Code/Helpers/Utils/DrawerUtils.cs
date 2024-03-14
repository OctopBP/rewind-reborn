using System;
using JetBrains.Annotations;
using UnityEditor;

namespace Code.Helpers.Utils
{
  [PublicAPI]
  public struct EditorIndent : IDisposable
  {
    private readonly int initialLevel;

    public EditorIndent(int wantedLevel)
    {
      initialLevel = EditorGUI.indentLevel;
      EditorGUI.indentLevel = wantedLevel;
    }
    
    public static EditorIndent Plus(int howMuch = 1) => new EditorIndent(EditorGUI.indentLevel + howMuch);

    public void Dispose()
    {
      EditorGUI.indentLevel = initialLevel;
    }
  }
}