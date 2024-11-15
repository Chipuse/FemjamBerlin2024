using UnityEngine;
using System.Collections;
public interface ITargetable
{
    public string INameOfTarget { get; set; }
    public void OnEnterNewAilment(Ailment _ailment);

    public void AffectHealth(int _deltaHealth);
/*
    public void TargetedByWater();
    public void AfterTargetedByWater();
    public void TargetedByHolyWater();
    public void AfterTargetedByHolyWater();
    public void TargetedByMedusasEye();
    public void AfterTargetedByMedusasEye();
    public void TargetedByIceGem();
    public void AfterTargetedByIceGem();
    public void TargetedByBandaid();
    public void AfterTargetedByBandaid();
    public void TargetedByStinger();
    public void AfterTargetedByStinger();
    public void TargetedByBasicAttack();
    public void AfterTargetedByBasicAttack();
    */
}

