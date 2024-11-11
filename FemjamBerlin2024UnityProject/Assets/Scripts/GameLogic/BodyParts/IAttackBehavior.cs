using System;
using UnityEngine;
[Serializable]
public class IAttackBehavior 
{
public BodyPart bodyPart;
   public virtual void EffectCheck(){
    bodyPart.DefaultAttack();

   }
}
