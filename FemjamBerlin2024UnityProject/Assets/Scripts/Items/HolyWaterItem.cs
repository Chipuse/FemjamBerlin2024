using UnityEngine;


public class HolyWaterItem : BaseItem
{
    public override void OnUseHero()
    {
       
       GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ItemEffectHero));
        MostTexts.mostTexts.FillTextBox(usedOnPlayerText);
        GameManager.gameManager.latestAttacker.AffectHealth(healPlayerAmount);
    }
}



