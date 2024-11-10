using UnityEngine;


public enum Ailment
{
    neutral,
    wet,
    banded,
    petrified,
    frozen,
    blind,
    dead
}


public enum Inputmode 
{ 
    menu,
    animation
}

public enum BodyPartEnum
{
    wing,
    mouth,
    arm,
    tail,
    eye
}

public enum Items
{
    water,
    holyWater,
    medEye,
    iceGem,
    bandaid,
    stinger,
    backPack
}

public enum ItemTextContext
{
    description,
    usedOnPlayer,
    effectOnPlayer,
    usedOnBoss,
    effectOnBoss,
}

public enum AttackTextContext
{
    whenCharging,
    beforeAttack,
    afterAttack,
    whenFrozen,
    whenPetrified,
    whenBanded,
    whenDead,
    specialCase
}

static class GlobalVariables
{
    public static float blinkTime = 0.05f;
}