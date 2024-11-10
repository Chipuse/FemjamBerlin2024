using UnityEngine;

public class WaterItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        GameManager.gameManager.hero.RemoveItem(Items.water);
        _target.TargetedByWater();
        base.OnUse(_target);
    }
}

public class HolyWaterItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        GameManager.gameManager.hero.RemoveItem(Items.holyWater);
        
        _target.TargetedByHolyWater();
        base.OnUse(_target);
    }
}

public class MedEyeItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        GameManager.gameManager.hero.RemoveItem(Items.medEye);
        _target.TargetedByMedusasEye();
        base.OnUse(_target);
    }
}

public class IceGemItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        GameManager.gameManager.hero.RemoveItem(Items.iceGem);
        _target.TargetedByIceGem();
        base.OnUse(_target);
    }
}

public class BandaidItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        GameManager.gameManager.hero.RemoveItem(Items.bandaid);
        _target.TargetedByBandaid();
        base.OnUse(_target);
    }
}

public class StingerItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        GameManager.gameManager.hero.RemoveItem(Items.stinger);
        _target.TargetedByStinger();
        base.OnUse(_target);
    }
}
