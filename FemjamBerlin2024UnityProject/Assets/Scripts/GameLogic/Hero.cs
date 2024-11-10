using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, ITargetable
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Ailment ailment = Ailment.neutral;

    public TextboxText OnAttackText;

    public GameObject healthbar;
    public void UpdateHealthBar()
    {
        float newScale = (float)currentHealth / (float)maxHealth;
        if (newScale < 0)
            newScale = 0;
        healthbar.transform.localScale = new Vector3(newScale, 1f, 1f);
    }

    //inventory
    public List<Items> inventory = new List<Items>();

    void Start()
    {
        inventory.Add(Items.water);
        inventory.Add(Items.medEye);
        inventory.Add(Items.iceGem);
        inventory.Add(Items.stinger);
        UpdateHealthBar();
    }

    void Update()
    {
        
    }

    public void OnPray()
    {
        //Heal some damage, turn water into holy water
    }

    public void OnAttackButton()
    {
        GameManager.gameManager.OpenCancelMenu();
        GameManager.gameManager.chosenItem = null;
    }

    public void OnAttack(ITargetable _target)
    {
        // deal damage to target
        _target.AffectHealth(-1500);
    }

    public void OnUseItem(BaseItem item)
    {

    }

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

    public string NameOfTarget = "sillyHero";
    public string INameOfTarget { get { return NameOfTarget; } set { NameOfTarget = INameOfTarget; } }

    public void OnEnterNewAilment(Ailment _ailment)
    {
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
        //activate vfx?
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
        AffectHealth(100);
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
        AffectHealth(10000);
    }

    void ITargetable.TargetedByStinger()
    {
        OnEnterNewAilment(Ailment.blind);
        AffectHealth(-30);
    }

    void ITargetable.TargetedByBasicAttack()
    {
        throw new System.NotImplementedException();
    }

    public void AffectHealth(int _deltaHealth)
    {
        int deltaHealth = _deltaHealth;

        if(deltaHealth + currentHealth > maxHealth) 
        {
            deltaHealth = maxHealth - currentHealth;
        }
        StartCoroutine(AffectHealthCoroutine(deltaHealth));        
    }

    private IEnumerator AffectHealthCoroutine(int _deltaHealth)
    {
        if(_deltaHealth < 0)
        {
            if(currentSprite != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    currentSprite.SetActive(false);
                    yield return new WaitForSeconds(GlobalVariables.blinkTime);
                    currentSprite.SetActive(true);
                    yield return new WaitForSeconds(GlobalVariables.blinkTime);
                }
            }
        }
        else
        {
            //its a heal play animation! opr not idc
        }
        int targetHealth = currentHealth + _deltaHealth;
        while(currentHealth != targetHealth)
        {
            yield return new WaitForEndOfFrame();
            if (_deltaHealth < 0)
                currentHealth--;
            else
                currentHealth++;
            UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            GameManager.gameManager.OnHeroDeath();
        }

    }

}
