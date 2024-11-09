using UnityEngine;

[CreateAssetMenu(fileName = "TextboxText", menuName = "Scriptable Objects/TextboxText")]
public class TextboxText : ScriptableObject
{
    public string text { get { return Parsed(text); } } 

    string Parsed(string _input)
    {
        return _input;
    }
}
