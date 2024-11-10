using UnityEngine;

[CreateAssetMenu(fileName = "TextboxText", menuName = "Scriptable Objects/TextboxText")]
public class TextboxText : ScriptableObject
{
    public string text = ""; 

    public string Parsed(string _input)
    {
        return _input;
    }

    public string Parsed()
    {
        return text;
    }
}
