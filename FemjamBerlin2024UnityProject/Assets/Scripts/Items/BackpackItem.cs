using UnityEngine;

public class BackpackItem : BaseItem
{
   public override void OnUse()
    {
      //  GameManager.gameManager.hero.RemoveItem(itemType);

        if (GameManager.gameManager.latestTarget == (ITargetable) GameManager.gameManager.hero)
        {
            OnUseHero();
        }
        else
        {
            OnUseBoss();
        }
    }
}
