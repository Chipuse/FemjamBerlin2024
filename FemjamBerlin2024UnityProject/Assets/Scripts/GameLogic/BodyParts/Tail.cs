using UnityEngine;

public class Tail : IAttackBehavior
{
    public override void EffectCheck(){
        if (GameManager.gameManager.hero.ailment == Ailment.petrified)
        {
            GameManager.gameManager.hero.AffectHealth(-1);
            MostTexts.mostTexts.FillTextBox(bodyPart.attacksPetrifiedText);
            bodyPart.KillThisBodyPart();
            GameManager.gameManager.hero.inventory.Add(Items.stinger);
        }
        else{
             GameManager.gameManager.hero.AffectHealth(-9999);
            MostTexts.mostTexts.FillTextBox(bodyPart.afterAttackText);
        }


    }
}
