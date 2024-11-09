using UnityEngine;

public class BaseItem
{
    public Sprite sprite;
    //displayed while hovering in Item menu
    public TextboxText textboxText;
    
    public void OnUse(ITargetable _target)
    {
        Debug.Log("Item was used on " + _target.INameOfTarget);
    }
}
