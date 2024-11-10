using System.Collections;
using UnityEngine;

public class BodyPart : MonoBehaviour, ITargetable
{
    public string NameOfTarget = "sillybean";
    public bool isOpenEye = false;
    public string INameOfTarget { get { return NameOfTarget; } set { NameOfTarget = INameOfTarget; } }

    public Ailment ailmentState = Ailment.neutral;

    public Enemy enemy;

    public GameObject spriteNeutral;
    public GameObject spritePetrified;
    public GameObject spriteWet;
    public GameObject spriteFrozen;
    public GameObject spriteBanded;
    public GameObject spriteDamaged;
    public GameObject spriteDead;
    public GameObject spriteBlind;

    public GameObject currentSprite;



    //potentially more stuff???
    private void Start()
    {
        if (enemy == null)
            enemy = GameManager.gameManager.enemy;
    }

    public void OnEnterNewAilment(Ailment _ailment)
    {
        ailmentState = _ailment;

        if (spriteNeutral == null)
            spriteNeutral = currentSprite;
        spriteNeutral?.SetActive(false);
        if (spritePetrified == null)
            spritePetrified = currentSprite;
        spritePetrified?.SetActive(false);
        if (spriteWet == null)
            spriteWet = currentSprite;
        spriteWet?.SetActive(false);
        if (spriteFrozen == null)
            spriteFrozen = currentSprite;
        spriteFrozen?.SetActive(false);
        if (spriteBanded == null)
            spriteBanded = currentSprite;
        spriteBanded?.SetActive(false);
        if (spriteDamaged == null)
            spriteDamaged = currentSprite;    
        spriteDamaged?.SetActive(false);
        if (spriteDead == null)
            spriteDead = currentSprite;
        spriteDead?.SetActive(false);
        if (spriteBlind == null)
            spriteBlind = currentSprite;
        spriteBlind?.SetActive(false);

        switch (_ailment)
        {
            case Ailment.neutral:
                spriteNeutral?.SetActive(true);
                currentSprite = spriteNeutral;
                break;
            case Ailment.wet:
                spriteWet?.SetActive(true);
                currentSprite = spriteWet;
                break;
            case Ailment.banded:
                spriteBanded?.SetActive(true);
                currentSprite = spriteBanded;
                break;
            case Ailment.petrified:
                spritePetrified?.SetActive(true);
                currentSprite = spritePetrified;
                break;
            case Ailment.frozen:
                spriteFrozen?.SetActive(true);
                currentSprite = spriteFrozen;
                break;
            case Ailment.dead:
                spriteDead?.SetActive(true);
                currentSprite = spriteDead;
                break;
            case Ailment.blind:
                spriteBlind?.SetActive(true);
                currentSprite = spriteBlind;
                break;
            default:
                break;
        }
    }

    void ITargetable.TargetedByWater()
    {
        OnEnterNewAilment(Ailment.wet);
    }

    void ITargetable.TargetedByHolyWater()
    {
        OnEnterNewAilment(Ailment.wet);
        KillThisBodyPart();
    }

    void ITargetable.TargetedByMedusasEye()
    {
        OnEnterNewAilment(Ailment.petrified);
    }

    void ITargetable.TargetedByIceGem()
    {
        OnEnterNewAilment(Ailment.frozen);
    }

    void ITargetable.TargetedByBandaid()
    {
        OnEnterNewAilment(Ailment.banded);
    }

    void ITargetable.TargetedByStinger()
    {
        // stinger should reduce the hp to 1 if the eye is the tarhget
        OnEnterNewAilment(Ailment.blind);
        if (isOpenEye)
        {
            AffectHealth(-enemy.currentHealth + 1);
        }
        else
        {
            AffectHealth(-30);
        }
    }

    void ITargetable.TargetedByBasicAttack()
    {
        //if body part is eye -> it will deal the last 1 damage so please make sure we already have 1 hp
        throw new System.NotImplementedException();
    }

    public void AffectHealth(int _deltaHealth)
    {
        StartCoroutine(AffectHealthCoroutine(_deltaHealth));

    }

    private IEnumerator AffectHealthCoroutine(int _deltaHealth)
    {
        if(currentSprite != null)
        {
            for (int i = 0; i < 10; i++)
            {
                currentSprite.SetActive(false);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
                currentSprite.SetActive(true);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
            }
        }
        int targetHealth = enemy.currentHealth + _deltaHealth;
        while (enemy.currentHealth != targetHealth)
        {
            //yield return new WaitForEndOfFrame();
            if (_deltaHealth < 0)
                enemy.currentHealth--;
            else
                enemy.currentHealth++;
            enemy.UpdateHealthBar();
        }
        if(_deltaHealth < -1000)
        {
            enemy.bodyPartCount--;
            enemy.CheckBodyParts();
            for (int i = 0; i < 10; i++)
            {
                currentSprite.SetActive(false);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
                currentSprite.SetActive(true);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
            }
            transform.gameObject.SetActive(false);
            //boom
        }
    }

    public void KillThisBodyPart()
    {
        AffectHealth(-1500);
    }
}
