using System.Collections;
using UnityEngine;

public class BodyPart : MonoBehaviour, ITargetable
{
    public string NameOfTarget = "sillybean";
    public string INameOfTarget { get { return NameOfTarget; } set { NameOfTarget = INameOfTarget; } }

    public Enemy enemy;

    public GameObject spriteNeutral;
    public GameObject spritePetrified;
    public GameObject spriteWet;
    public GameObject spriteFrozen;
    public GameObject spriteBanded;
    public GameObject spriteDamaged;
    public GameObject spriteDead;
    public GameObject currentSprite;



    //potentially more stuff???

    public void OnEnterNewAilment(Ailment _ailment)
    {
        spriteNeutral.SetActive(false);
        spritePetrified.SetActive(false);
        spriteWet.SetActive(false);
        spriteFrozen.SetActive(false);
        spriteBanded.SetActive(false);
        spriteDamaged.SetActive(false);
        spriteDead.SetActive(false);

        switch (_ailment)
        {
            case Ailment.neutral:
                spriteNeutral.SetActive(true);
                currentSprite = spriteNeutral;
                break;
            case Ailment.wet:
                spriteWet.SetActive(true);
                currentSprite = spriteWet;
                break;
            case Ailment.banded:
                spriteBanded.SetActive(true);
                currentSprite = spriteBanded;
                break;
            case Ailment.petrified:
                spritePetrified.SetActive(true);
                currentSprite = spritePetrified;
                break;
            case Ailment.frozen:
                spriteFrozen.SetActive(true);
                currentSprite = spriteFrozen;
                break;
            case Ailment.dead:
                spriteDead.SetActive(true);
                currentSprite = spriteDead;
                break;
            default:
                break;
        }
    }

    void ITargetable.TargetedByWater()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByHolyWater()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByMedusasEye()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByIceGem()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByBandaid()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByStinger()
    {
        // stinger should reduce the hp to 1 if the eye is the tarhget
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByBasicAttack()
    {
        //if body part is eye -> it will deal the last 1 damage so please make sure we already have 1 hp
        throw new System.NotImplementedException();
    }

    void ITargetable.AffectHealth(int _deltaHealth)
    {
        StartCoroutine(AffectHealthCoroutine(_deltaHealth));

    }

    private IEnumerator AffectHealthCoroutine(int _deltaHealth)
    {
        for (int i = 0; i < 10; i++)
        {
            currentSprite.SetActive(false);
            yield return new WaitForSeconds(GlobalVariables.blinkTime);
            currentSprite.SetActive(true);
            yield return new WaitForSeconds(GlobalVariables.blinkTime);
        }
        int targetHealth = enemy.currentHealth + _deltaHealth;
        while (enemy.currentHealth != targetHealth)
        {
            yield return new WaitForEndOfFrame();
            if (_deltaHealth < 0)
                enemy.currentHealth--;
            else
                enemy.currentHealth++;
            enemy.UpdateHealthBar();
        }

    }
}
