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
    stinger
}

static class GlobalVariables
{
    public static float blinkTime = 0.05f;
}