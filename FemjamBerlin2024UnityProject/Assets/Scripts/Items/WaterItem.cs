using UnityEngine;

public class WaterItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        _target.TargetedByWater();
        base.OnUse(_target);
    }
}

public class HolyWaterItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        _target.TargetedByHolyWater();
        base.OnUse(_target);
    }
}

public class MedEyeItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        _target.TargetedByMedusasEye();
        base.OnUse(_target);
    }
}

public class IceGemItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        _target.TargetedByIceGem();
        base.OnUse(_target);
    }
}

public class BandaidItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        _target.TargetedByBandaid();
        base.OnUse(_target);
    }
}

public class StingerItem : BaseItem
{
    public override void OnUse(ITargetable _target)
    {
        _target.TargetedByStinger();
        base.OnUse(_target);
    }
}
