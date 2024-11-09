using UnityEngine;

public interface ITargetable
{
    public string INameOfTarget { get; set; }
    public void OnEnterNewAilment(Ailment _ailment);
}
