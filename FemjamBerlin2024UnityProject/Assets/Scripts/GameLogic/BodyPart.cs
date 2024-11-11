using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPart : MonoBehaviour, ITargetable
{
    public string NameOfTarget = "sillybean";
    public bool isOpenEye = false;
    public string INameOfTarget { get { return NameOfTarget; } set { NameOfTarget = INameOfTarget; } }

    public Ailment ailmentState = Ailment.neutral;

    public Enemy enemy;

    public CharacterSprite[] spriteList;
    public Dictionary<Ailment, Sprite> spriteDic;

    public string chargingText;
    public string beforeAttackText;
    public string afterAttackText;
    public string isFrozenText;
    public string isPetrifiedText;
    public string isBandedText;
    public string isDeadText;
    public string attacksPetrifiedText;



    /*

    public GameObject spriteNeutral;
    public GameObject spritePetrified;
    public GameObject spriteWet;
    public GameObject spriteFrozen;
    public GameObject spriteBanded;
    public GameObject spriteDamaged;
    public GameObject spriteDead;
    public GameObject spriteBlind;
*/
    public GameObject currentSprite;



    //potentially more stuff???
    private void Start()
    {
        if (enemy == null)
            enemy = GameManager.gameManager.enemy;

        spriteDic = spriteList[0].GetDic(spriteList);
    }

    public void OnEnterNewAilment(Ailment _ailment)
    {
        ailmentState = _ailment;
        currentSprite.GetComponent<Image>().sprite = spriteDic.GetValueOrDefault(_ailment);

        /*
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
                */
    }

    void ITargetable.TargetedByWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByWater));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.water, ItemTextContext.usedOnBoss));
    }

    void ITargetable.TargetedByHolyWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByHolyWater));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.holyWater, ItemTextContext.usedOnBoss));
    }

    void ITargetable.TargetedByMedusasEye()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByMedusasEye));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.medEye, ItemTextContext.usedOnBoss));
    }

    void ITargetable.TargetedByIceGem()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByIceGem));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.iceGem, ItemTextContext.usedOnBoss));
    }

    void ITargetable.TargetedByBandaid()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByBandaid));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.bandaid, ItemTextContext.usedOnBoss));
    }

    void ITargetable.TargetedByStinger()
    {

        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByStinger));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.stinger, ItemTextContext.usedOnBoss));
    }

    void ITargetable.TargetedByBasicAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByBasicAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.backPack, ItemTextContext.usedOnBoss));
    }

    public void AffectHealth(int _deltaHealth)
    {
        StartCoroutine(AffectHealthCoroutine(_deltaHealth));

    }

    private IEnumerator AffectHealthCoroutine(int _deltaHealth)
    {
        if (currentSprite != null)
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
        if (_deltaHealth < -1000 && enemy.bodyPartCount > 1)
        {
            enemy.bodyPartCount--;
            for (int i = 0; i < 10; i++)
            {
                currentSprite.SetActive(false);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
                currentSprite.SetActive(true);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
            }
            transform.gameObject.SetActive(false);
            enemy.CheckBodyParts();
            //boom
        }
        if (enemy.currentHealth <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                currentSprite.SetActive(false);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
                currentSprite.SetActive(true);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
            }
            currentSprite.SetActive(false);
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.WinGame));
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenDead));
        }
    }

    public void KillThisBodyPart()
    {
        AffectHealth(-1500);
    }

    public void AfterTargetedByWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.water, ItemTextContext.effectOnBoss));
        OnEnterNewAilment(Ailment.wet);
    }

    public void AfterTargetedByHolyWater()
    {
        KillThisBodyPart();
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.holyWater, ItemTextContext.effectOnBoss));
        OnEnterNewAilment(Ailment.wet);
    }

    public void AfterTargetedByMedusasEye()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.medEye, ItemTextContext.effectOnBoss));
        OnEnterNewAilment(Ailment.petrified);
    }

    public void AfterTargetedByIceGem()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.iceGem, ItemTextContext.effectOnBoss));
        OnEnterNewAilment(Ailment.frozen);
    }

    public void AfterTargetedByBandaid()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.bandaid, ItemTextContext.effectOnBoss));
        OnEnterNewAilment(Ailment.banded);
    }

    public void AfterTargetedByStinger()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.stinger, ItemTextContext.effectOnBoss));
        // stinger should reduce the hp to 1 if the eye is the tarhget
        OnEnterNewAilment(Ailment.blind);
        if (isOpenEye)
        {
            AffectHealth(-enemy.currentHealth + 10);
        }
        else
        {
            AffectHealth(-30);
        }
    }

    public void AfterTargetedByBasicAttack()
    {
        AffectHealth(-10);
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.backPack, ItemTextContext.effectOnBoss));
        //if body part is eye -> it will deal the last 1 damage so please make sure we already have 1 hp
    }


   
}

[Serializable]
public class CharacterSprite
{

    [SerializeField]
    Ailment ailment;
    [SerializeField]
    Sprite sprite;

    public Dictionary<Ailment, Sprite> GetDic(CharacterSprite[] sList)
    {
        Dictionary<Ailment, Sprite> dic = new Dictionary<Ailment, Sprite>();
        foreach (CharacterSprite cSprite in sList)
        {
            dic.Add(cSprite.ailment, cSprite.sprite);

        }
        return dic;
    }


}
