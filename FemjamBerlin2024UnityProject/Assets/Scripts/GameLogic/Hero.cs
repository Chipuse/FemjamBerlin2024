using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, ITargetable
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Ailment ailment = Ailment.neutral;


    public bool holyWet = false;
    public TextboxText prayText;

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
        inventory.Add(Items.bandaid);
        UpdateHealthBar();
    }

   

    public void RemoveItem(Items _item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] == _item)
            {
                inventory.RemoveAt(i);
                break;
            }
        }
    }

    void Update()
    {
        
    }

    public void OnPray()
    {
        //Heal some damage, turn water into holy water
        AffectHealth(10);
        if (ailment == Ailment.wet)
            holyWet = true;
        foreach (var item in inventory)
        {
            if(item == Items.water)
            {
                inventory.Remove(Items.water);
                inventory.Add(Items.holyWater);
                break;
            }
        }
        if(prayText != null)
        {
            MostTexts.mostTexts.FillTextBox(prayText);
        }
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
    }

    public void OnAttackButton()
    {
        GameManager.gameManager.OpenCancelMenu();

        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.backPack, ItemTextContext.description));
        GameManager.gameManager.chosenItem = null;
    }
/*
    public void OnAttack(ITargetable _target)
    {
        // deal damage to target
        _target.AfterTargetedByBasicAttack();
    }
*/
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
        ailment = _ailment;

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
/*
    void ITargetable.TargetedByWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByWater));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText( Items.water, ItemTextContext.usedOnPlayer));

        //fill textbox
    }

    void ITargetable.TargetedByHolyWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByHolyWater));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.holyWater, ItemTextContext.usedOnPlayer));

    }

    void ITargetable.TargetedByMedusasEye()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByMedusasEye));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.medEye, ItemTextContext.usedOnPlayer));

    }

    void ITargetable.TargetedByIceGem()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByIceGem));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.iceGem, ItemTextContext.usedOnPlayer));
    }

    void ITargetable.TargetedByBandaid()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByBandaid));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.bandaid, ItemTextContext.usedOnPlayer));
    }

    void ITargetable.TargetedByStinger()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByStinger));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.stinger, ItemTextContext.usedOnPlayer));
    }

    void ITargetable.TargetedByBasicAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTargetedByBasicAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.backPack, ItemTextContext.usedOnPlayer));
    }
*/
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
            //yield return new WaitForEndOfFrame();
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
/*
    public void AfterTargetedByWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.water, ItemTextContext.effectOnPlayer));
        OnEnterNewAilment(Ailment.wet);
    }

    public void AfterTargetedByHolyWater()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.holyWater, ItemTextContext.effectOnPlayer));
        OnEnterNewAilment(Ailment.wet);
        AffectHealth(100);
    }

    public void AfterTargetedByMedusasEye()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.medEye, ItemTextContext.effectOnPlayer));
        OnEnterNewAilment(Ailment.petrified);
    }

    public void AfterTargetedByIceGem()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.iceGem, ItemTextContext.effectOnPlayer));
        OnEnterNewAilment(Ailment.frozen);
    }

    public void AfterTargetedByBandaid()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.bandaid, ItemTextContext.effectOnPlayer));
        OnEnterNewAilment(Ailment.banded);
        AffectHealth(10000);
    }

    public void AfterTargetedByStinger()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.stinger, ItemTextContext.effectOnPlayer));
        OnEnterNewAilment(Ailment.blind);
        AffectHealth(-30);
    }
*/
    public void AfterTargetedByBasicAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(Items.backPack, ItemTextContext.effectOnPlayer));
        AffectHealth(-10);
    }
    
}
