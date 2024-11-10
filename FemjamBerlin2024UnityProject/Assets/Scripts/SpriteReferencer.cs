using System.Collections.Generic;
using UnityEngine;

public class SpriteReferencer : MonoBehaviour
{
    public static SpriteReferencer spriteReferencer;


    public Sprite waterItem;
    public Sprite holyWaterItem;
    public Sprite medEyeItem;
    public Sprite iceGemItem;
    public Sprite bandaidItem;
    public Sprite stingerItem;

    public Dictionary<Items, Sprite> ItemSpriteDict = new Dictionary<Items, Sprite>();
    private void Awake()
    {
        if(spriteReferencer == null)
        {
            spriteReferencer = this;
        }
        else
        {
            Destroy(this);
        }

        ItemSpriteDict.Add(Items.water, waterItem);
        ItemSpriteDict.Add(Items.holyWater, holyWaterItem);
        ItemSpriteDict.Add(Items.medEye, medEyeItem);
        ItemSpriteDict.Add(Items.iceGem, iceGemItem);
        ItemSpriteDict.Add(Items.bandaid, bandaidItem);
        ItemSpriteDict.Add(Items.stinger, stingerItem);
    }
}
