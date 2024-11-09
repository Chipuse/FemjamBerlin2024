using UnityEngine;
using System.Collections;
public interface ITargetable
{
    public string INameOfTarget { get; set; }
    public void OnEnterNewAilment(Ailment _ailment);

    public void AffectHealth(int _deltaHealth);

    public void TargetedByWater();
    public void TargetedByHolyWater();
    public void TargetedByMedusasEye();
    public void TargetedByIceGem();
    public void TargetedByBandaid();
    public void TargetedByStinger();
    public void TargetedByBasicAttack();
}
