using Unity.VisualScripting.Antlr3.Runtime;
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
        char[] separators = { '{', '}' };
        string[] strValues = text.Split(separators);
        string resultString = "";

        foreach (string str in strValues)
        {
            if (str == "TARGET")
            {
                resultString += GameManager.gameManager.latestTarget.INameOfTarget;
            }
            else if (str == "ATTACKER"){
                resultString+= GameManager.gameManager.latestAttacker.INameOfTarget;
            }
            else
            {
                resultString += str;
            }
        }
        return resultString;
    }

}
